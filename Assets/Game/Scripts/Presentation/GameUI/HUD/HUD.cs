using UnityEngine;
using Application;
using Infrastructure;

namespace Presentation
{
    public class HUD : Screen
    {
        [SerializeField] private HealthPanel _healthPanel;
        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private ShieldView _shieldView;
        [SerializeField] private ProgressBarView _progressBarView;

        private ProgressionService _progression;
        private WalletService _wallet;
        private Player _player;
        private EnemyDeathHandler _handler;
        private UpgradeSystem _upgradeSystem;

        public void Construct(Player player, WalletService wallet, ProgressionService progression, EnemyDeathHandler handler, UpgradeSystem upgradeSystem)
        {
            _progression = progression;
            _player = player;
            _wallet = wallet;
            _handler = handler;
            _upgradeSystem = upgradeSystem;

            _handler.OnEnemyDied += Refresh;
            _progression.OnProgression += Refresh;
            _wallet.OnWalletChanged += (value, _) => _coinsView.ShowCoinsText(value);
            _upgradeSystem.OnUpgrade += UpdateHealth;
            _upgradeSystem.OnUpgrade += Refresh;
        }

        public override void Dispose()
        {
            base.Dispose();
            _progression.OnProgression -= Refresh;
            _wallet.OnWalletChanged -= (value, _) => _coinsView.ShowCoinsText(value);
            _handler.OnEnemyDied -= Refresh;
            _upgradeSystem.OnUpgrade -= Refresh;
            _upgradeSystem.OnUpgrade -= UpdateHealth;
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
            _progressBarView.UpdateProgressbar(_progression.CurrentExperience, _progression.MaxUpgrade);
            _healthPanel.ChangeHealth(_player.CurrentHealth);

            if (_player.Shield) _shieldView.UpdateCoolDown(_player.Shield.CurrentCoolDownCount, _player.Shield.CoolDown);
        }

        public override void Reset()
        {
            _shieldView.Deactivate();
            Refresh();
            _healthPanel.Reset();
        }

        private void UpdateHealth()
        {
            _healthPanel.UpdateHealth(_player.MaxHealth);
            _shieldView.transform.SetAsLastSibling();
        }
    }
}