using System;
using UnityEngine;
using Contracts;

namespace Game
{
    public class PlayerPresenter : ITickable, IPlayer
    {
        private readonly WeaponSystem _weapon;
        private readonly PlayerInputController _inputController;
        private readonly PlayerMovement _movement;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private readonly PlayerPickupHandler _pickupHandler;
        private readonly PlayerDamageHandler _damageHandler;

        public PlayerPresenter(
            PlayerView view,
            PlayerMovement movement,
            PlayerInputController inputController,
            PlayerModel model,
            PlayerPickupHandler pickupHandler,
            PlayerDamageHandler damageHandler,
            WeaponSystem weapon
        )
        {
            _view = view;
            _movement = movement;
            _inputController = inputController;
            _model = model;
            _pickupHandler = pickupHandler;
            _damageHandler = damageHandler;
            _weapon = weapon;
        }

        public Vector3 Position => _view.Position;

        public event Action OnDied;

        public void Initialize()
        {
            _view.OnTrigger += OnTrigger;
            _model.OnDied += Die;
        }

        public void Dispose()
        {
            _view.OnTrigger -= OnTrigger;
            _model.OnDied -= Die;
        }

        public void SetPlayerStats(SpartanStats stats) => _model.SetPlayerStats(stats);

        public void StartPlay()
        {
            _model.StartPlay();
            _inputController.Enable();
        }

        public void Tick(float dt)
        {
            if (_model.IsDead || !_model.IsPlaying) return;

            _weapon.ApplyAll(dt);
            _view.SetPosition(_movement.GetPosition(_view.Position, _view.HalfSize, dt));
        }

        public void Restart()
        {
            _view.ShowAlive();
            _model.Restart();
            _weapon.Reset();
            _view.SetPosition(Vector3.zero);
            _damageHandler.Restart();
        }

        private void Die()
        {
            _inputController.Disable();
            _view.ShowDead();
            OnDied?.Invoke();
        }

        private void OnTrigger(Collider2D other) => _pickupHandler.Handle(other);
    }
}