using UnityEngine;

namespace Game
{
    public class HUD : Core.UI.Screen
    {
        [SerializeField] private HealthPanel _healthPanel;
        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private ShieldViewValues _shieldViewValues;
        [SerializeField] private ProgressBarView _progressBarView;

        private IProgressionReadModel _progressionModel;
        private IWallet _wallet;
        private IPlayerReadModel _player;
        private IShieldReadModel _shield;
        private IUpgrade _upgrade;

        public void Construct(IPlayerReadModel playerModel, IShieldReadModel shield, IWallet wallet, IProgressionReadModel progressionModel, IUpgrade upgrade)
        {
            _progressionModel = progressionModel;
            _player = playerModel;
            _wallet = wallet;
            _shield = shield;
            _upgrade = upgrade;

            _wallet.OnChanged += UpdateValues;
            _player.OnHealthChanged += UpdateHealthValues;
            _shield.OnShieldStateChanged += UpdateShieldValues;
            _progressionModel.OnProgressChanged += UpdateProgress;
            _upgrade.OnUpgraded += UpdateValues;
            _upgrade.OnUpgraded += UpdateHealth; 
            _upgrade.OnUpgraded += UpdateShieldValues;
        }

        public override void Dispose()
        {
            base.Dispose();
            _wallet.OnChanged -= UpdateValues;
            _player.OnHealthChanged -= UpdateHealthValues;
            _shield.OnShieldStateChanged -= UpdateShieldValues;
            _progressionModel.OnProgressChanged -= UpdateProgress;
            _upgrade.OnUpgraded -= UpdateValues;
            _upgrade.OnUpgraded -= UpdateHealth;
            _upgrade.OnUpgraded -= UpdateShieldValues;
        }

        public override void Show()
        {
            base.Show();
            UpdateHealth();
            UpdateHealthValues();
            UpdateValues();
        }

        public override void Reset()
        {
            UpdateProgress();
            _shieldViewValues.Deactivate();
            _healthPanel.Reset();
        }

        private void UpdateHealthValues() => _healthPanel.ChangeHealth(_player.CurrentHealth);
        private void UpdateValues() => _coinsView.ShowCoinsText(_wallet.Coins);
        private void UpdateProgress() => _progressBarView.UpdateProgressbar(_progressionModel.CurrentExperience, _progressionModel.MaxUpgrade);

        private void UpdateShieldValues()
        {
            if (_shield.Shield != null)
                _shieldViewValues.UpdateCoolDown(_shield.Shield.CurrentCoolDownCount, _shield.Shield.CoolDown);
        }
        
        private void UpdateHealth()
        {
            _healthPanel.UpdateHealth(_player.MaxHealth);
            _shieldViewValues.transform.SetAsLastSibling();
        }
    }
}