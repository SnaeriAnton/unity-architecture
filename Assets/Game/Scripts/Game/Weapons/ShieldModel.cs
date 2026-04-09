using R3;

namespace Game
{
    public class ShieldModel : IShieldReadModel
    {
        private readonly ReactiveProperty<bool> _hasShield = new();
        private readonly ReactiveProperty<int> _currentCoolDownCount = new();
        private readonly ReactiveProperty<int> _coolDown = new();
        
        private WeaponStats _stats;
        
        public ReadOnlyReactiveProperty<bool> HasShield => _hasShield;
        public ReadOnlyReactiveProperty<int> CurrentCoolDownCount => _currentCoolDownCount;
        public ReadOnlyReactiveProperty<int> CoolDown => _coolDown;
        public bool ShieldIsActive => _stats.CoolDown == _currentCoolDownCount.Value;

        public void Init(WeaponStats stats)
        {
            _stats = stats;
            _coolDown.Value = stats.CoolDown;
            _currentCoolDownCount.Value = _stats.CoolDown;
            _hasShield.Value = true;
        }

        public void Apply() => _currentCoolDownCount.Value = 0;
        public void RefreshState() => _currentCoolDownCount.Value++;

        public void Reset()
        {
            _hasShield.Value = false;
            _stats = default;
        }
    }
}