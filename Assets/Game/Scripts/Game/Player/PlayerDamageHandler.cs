using UnityEngine;
using Zenject;

namespace Game
{
    public class PlayerDamageHandler : ITickable
    {
        private readonly PlayerModel _model;
        private readonly WeaponSystem _weapon;
        private readonly float _iFramesDuration;

        private float _invulnTimer;
        
        public PlayerDamageHandler(PlayerModel model, WeaponSystem weapon, PlayerStats stats)
        {
            _model = model;
            _iFramesDuration = stats.IFramesDuration;
            _weapon = weapon;
        }

        public bool IsDead => _model.IsDead;
        
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

        public void Tick()
        {
            if (_model.IsDead || !_model.IsPlaying) return;

            if (IsInvulnerable)
                _invulnTimer -= Time.deltaTime;
        }
    }
}