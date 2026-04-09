using System;
using Contracts;
using R3;

namespace Game
{
    public class PlayerDamageHandler : IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly PlayerModel _model;
        private readonly WeaponSystem _weapon;
        private readonly IUpdateStream _updateStream;
        private readonly float _iFramesDuration;

        private float _invulnTimer;
        
        public PlayerDamageHandler(PlayerModel model, WeaponSystem weapon, IUpdateStream updateStream, float iFramesDuration)
        {
            _model = model;
            _weapon = weapon;
            _updateStream = updateStream;
            _iFramesDuration = iFramesDuration;

            _updateStream.OnUpdate.Subscribe(Tick).AddTo(_disposable);
        }

        public bool IsDead => _model.IsDead.CurrentValue;
        
        private bool IsInvulnerable => _invulnTimer > 0f;
        
        public void Restart() => _invulnTimer = 0;
        
        public void TakeDamage(int damage)
        {
            if (IsInvulnerable) return;
            if (_weapon.Shield && _weapon.Shield.TryApply())
                return;

            _invulnTimer = _iFramesDuration;
            _model.TakeDamage(damage);
        }

        public void Tick(float dt)
        {
            if (_model.IsDead.CurrentValue || !_model.IsPlaying.CurrentValue) return;

            if (IsInvulnerable)
                _invulnTimer -= dt;
        }

        public void Dispose() => _disposable.Dispose();
    }
}