namespace Game
{
    public class ShieldModel : WeaponModel
    {
        private bool _isActive = true;
        
        public int CurrentCoolDownCount { get; private set; }
        public int CoolDown => Stats.CoolDown;

        private bool ShieldIsActive => Stats.CoolDown == CurrentCoolDownCount;
        
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

        public override void Reset()
        {
            base.Reset();
            _isActive = false;
        }

        public override void SetStats(WeaponStats stats)
        {
            base.SetStats(stats);
            _isActive = true;
            CurrentCoolDownCount = Stats.CoolDown;
        }

        public void UpdateValues()
        {
            if (ShieldIsActive || !_isActive) return;

            CurrentCoolDownCount++;
        }
    }
    
}