using Contracts;

namespace Game
{
    public class PlayerDamageHandler : ITickable
    {
        private readonly PlayerModel _model;
        private readonly WeaponSystem _weapon;
        private readonly float _iFramesDuration;

        private float _invulnTimer;
        
        public PlayerDamageHandler(PlayerModel model, WeaponSystem weapon, float iFramesDuration)
        {
            _model = model;
            _iFramesDuration = iFramesDuration;
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

        public void Tick(float dt)
        {
            if (_model.IsDead || !_model.IsPlaying) return;

            if (IsInvulnerable)
                _invulnTimer -= dt;
        }
    }
}