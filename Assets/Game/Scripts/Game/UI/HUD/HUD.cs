using UnityEngine;

namespace Game
{
    public class HUD : Core.UI.Screen
    {
        [SerializeField] private HealthPanel _healthPanel;
        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private ShieldView _shieldView;
        [SerializeField] private ProgressBarView _progressBarView;

        private ProgressionSystem _progressionSystem;
        private Wallet _wallet;
        private PlayerHealthView _playerHealthView;
        private PlayerShieldView _playerShieldBridge;

        public void Construct(PlayerHealthView playerHealthView, PlayerShieldView playerShieldView, Wallet wallet, ProgressionSystem progressionSystem)
        {
            _progressionSystem = progressionSystem;
            _playerShieldBridge = playerShieldView;
            _playerHealthView = playerHealthView;
            _wallet = wallet;
        }

        public override void Show()
        {
            base.Show();
            UpdateHealth();
            Refresh();
        }

        public void Refresh()
        {
            if (!gameObject.activeSelf) return;
            _progressBarView.UpdateProgressbar(_progressionSystem.CurrentExperience, _progressionSystem.MaxUpgrade);
            _healthPanel.ChangeHealth(_playerHealthView.CurrentHealth);
            _coinsView.ShowCoinsText(_wallet.Coins);

            if (_playerShieldBridge.IsActive) _shieldView.UpdateCoolDown(_playerShieldBridge.Current, _playerShieldBridge.Cooldown);
        }

        public override void Reset()
        {
            _shieldView.Deactivate();
            Refresh();
            _healthPanel.Reset();
        }

        private void UpdateHealth()
        {
            _healthPanel.UpdateHealth(_playerHealthView.MaxHealth);
            _shieldView.transform.SetAsLastSibling();
        }
    }
}