using System;
using UnityEngine;
using Contracts;
using UniRx;

namespace Game
{
    public class PlayerController : IPlayer, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly PlayerViewModel _viewModel;
        private readonly WeaponSystem _weapon;
        private readonly PlayerInputController _inputController;
        private readonly PlayerMovement _movement;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private readonly PlayerPickupHandler _pickupHandler;
        private readonly PlayerDamageHandler _damageHandler;
        private readonly IUpdateStream _updateStream;
        
        public PlayerController(
            PlayerViewModel viewModel,
            PlayerView view,
            PlayerMovement movement,
            PlayerInputController inputController,
            PlayerModel model,
            PlayerPickupHandler pickupHandler,
            PlayerDamageHandler damageHandler,
            WeaponSystem weapon,
            IUpdateStream updateStream
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
            _updateStream = updateStream;
            
            _updateStream.OnUpdate.Subscribe(Tick).AddTo(_disposable);
        }

        public void Initialize()
        {
            _view.OnTrigger.Subscribe(OnTrigger).AddTo(_disposable);
            _model.OnDied.Subscribe(_ => Die()).AddTo(_disposable);
        }

        public void Dispose() => _disposable.Dispose();

        public void SetPlayerStats(SpartanStats stats) => _model.SetPlayerStats(stats);

        public void StartPlay()
        {
            _model.StartPlay();
            _inputController.Enable();
        }
        
        public void Restart()
        {
            _model.Restart();
            _weapon.Reset();
            _damageHandler.Restart();
            _model.SetPosition(Vector3.zero);
        }

        private void Tick(float dt)
        {
            if (_model.IsDead || !_model.IsPlaying.Value) return;

            _weapon.ApplyAll(dt);
            Vector3 position = _movement.GetPosition(_view.Position, _view.HalfSize, dt);
            _model.SetPosition(position);
        }
        
        private void OnTrigger(Collider2D other) => _pickupHandler.Handle(other);

        private void Die() => _inputController.Disable();
    }
}