namespace Game
{
    public class Shield : Weapon
    {
        private ShieldModel _shieldModel;
        private bool _isActive = true;

        public override void SetStats(WeaponStats stats)
        {
            base.SetStats(stats);
            _isActive = true;
            _shieldModel.Init(_stats);
        }

        public void SetShieldModel(ShieldModel shieldModel)
        {
            _shieldModel = shieldModel;
            _shieldModel.Init(_stats);
        }

        public bool TryApply()
        {
            if (!_isActive) return false;

            if (_shieldModel.ShieldIsActive)
            {
                _shieldModel.Apply();
                return true;
            }

            return false;
        }

        public override void RefreshState()
        {
            if (_shieldModel.ShieldIsActive || !_isActive) return;

            _shieldModel.RefreshState();
        }

        public override void Reset()
        {
            base.Reset();
            _shieldModel.Reset();
            _isActive = false;
        }
    }
}