using System;

namespace Game
{
    public class ShieldModel : IShieldReadModel
    {
        private WeaponStats _stats;

        public bool ShieldIsActive => _stats.CoolDown == CurrentCoolDownCount;
        public bool HasShield { get; private set; }
        public int CurrentCoolDownCount { get; private set; }
        public int CoolDown => _stats.CoolDown;

        public event Action OnShieldStateChanged;

        public void Init(WeaponStats stats)
        {
            _stats = stats;
            CurrentCoolDownCount = _stats.CoolDown;
            HasShield = true;
            OnShieldStateChanged?.Invoke();
        }

        public void Apply()
        {
            CurrentCoolDownCount = 0;
            OnShieldStateChanged?.Invoke();
        }

        public void RefreshState()
        {
            CurrentCoolDownCount++;
            OnShieldStateChanged?.Invoke();
        }

        public void Reset()
        {
            HasShield = false;
            _stats = default;
        }
    }
}