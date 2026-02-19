namespace Game
{
    public class Shield : Weapon
    {
        private bool _isActive = true;
        
        public int CurrentCoolDownCount { get; private set; }
        public int CoolDown => _stats.CoolDown;

        private bool ShieldIsActive => _stats.CoolDown == CurrentCoolDownCount;

        public override void SetStats(WeaponStats stats)
        {
            base.SetStats(stats);
            _isActive = true;
            CurrentCoolDownCount = _stats.CoolDown;
        }

        public bool TryApply()
        {
            if (!_isActive) return false;
            
            if (ShieldIsActive)
            {
                CurrentCoolDownCount = 0;
                return true;
            }

            return false;
        }

        public override void UpdateValues()
        {
            if (ShieldIsActive || !_isActive) return;

            CurrentCoolDownCount++;
        }

        public override void Reset()
        {
            base.Reset();
            _isActive = false;
        }
    }
}