using System;
using UnityEngine;
using Contracts;

namespace Game
{
    public class PlayerController : ITickable, IPlayer
    {
        private readonly PlayerViewModel _viewModel;
        private readonly WeaponSystem _weapon;
        private readonly PlayerInputController _inputController;
        private readonly PlayerMovement _movement;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private readonly PlayerPickupHandler _pickupHandler;
        private readonly PlayerDamageHandler _damageHandler;

        public PlayerController(
            PlayerViewModel viewModel,
            PlayerView view,
            PlayerMovement movement,
            PlayerInputController inputController,
            PlayerModel model,
            PlayerPickupHandler pickupHandler,
            PlayerDamageHandler damageHandler,
            WeaponSystem weapon
        )
        {
            _viewModel = viewModel;
            _view = view;
            _movement = movement;
            _inputController = inputController;
            _model = model;
            _pickupHandler = pickupHandler;
            _damageHandler = damageHandler;
            _weapon = weapon;
        }

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
            _viewModel.RefreshState();
        }

        public void Tick(float dt)
        {
            if (_model.IsDead || !_model.IsPlaying) return;

            _weapon.ApplyAll(dt);
            Vector3 position = _movement.GetPosition(_view.Position, _view.HalfSize, dt);
            _model.SetPosition(position);
            _viewModel.SetPosition(position);
        }

        public void Restart()
        {
            _model.Restart();
            _viewModel.RefreshState();
            _weapon.Reset();
            _viewModel.SetPosition(Vector3.zero);
            _damageHandler.Restart();
        }

        private void OnTrigger(Collider2D other) => _pickupHandler.Handle(other);

        private void Die()
        {
            _inputController.Disable();
            OnDied?.Invoke();
        }
    }
}