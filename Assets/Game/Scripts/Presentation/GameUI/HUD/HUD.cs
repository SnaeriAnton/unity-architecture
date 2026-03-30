using Application;
using UnityEngine;

namespace Presentation
{
    public class HUD : Screen
    {
        [SerializeField] private HealthPanel _healthPanel;
        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private ShieldView _shieldView;
        [SerializeField] private ProgressBarView _progressBarView;

        private IProgressionReadModel _progression;
        private IWalletReadModel _wallet;
        private IPlayerReadModel _player;
        private IShieldReadModel _shield;
        private IEnemyDeathHandler _enemyDeathHandler;
        private IUpgradeReadModel _upgrade;

        public void Construct(IPlayerReadModel player, IWalletReadModel wallet, IProgressionReadModel progression, IShieldReadModel shield, IEnemyDeathHandler enemyDeathHandler, IUpgradeReadModel upgrade)
        {
            _progression = progression;
            _player = player;
            _wallet = wallet;
            _shield = shield;
            _enemyDeathHandler = enemyDeathHandler;
            _upgrade = upgrade;

            _wallet.OnCoinsChanged += Refresh;
            _enemyDeathHandler.OnEnemyDead += Refresh;
            _upgrade.OnUpgrade += Refresh;
            _progression.OnPickUpCrystal += Refresh;
            _progression.OnUpgradeStats += Refresh;
            _shield.OnShieldChanged += Refresh;
            _player.OnHealthChanged += Refresh;
            _player.OnUpgradeStats += UpdateHealth;
        }

        public override void Dispose()
        {
            base.Dispose();
            _wallet.OnCoinsChanged -= Refresh;
            _enemyDeathHandler.OnEnemyDead -= Refresh;
            _upgrade.OnUpgrade -= Refresh;
            _progression.OnPickUpCrystal -= Refresh;
            _progression.OnUpgradeStats -= Refresh;
            _shield.OnShieldChanged -= Refresh;
            _player.OnHealthChanged -= Refresh;
            _player.OnUpgradeStats -= UpdateHealth;
        }

        public override void Show()
        {
            base.Show();
            UpdateHealth();
            Refresh();
        }

        public override void Reset()
        {
            _shieldView.Deactivate();
            Refresh();
            _healthPanel.Reset();
        }

        private void Refresh()
        {
            if (!gameObject.activeSelf) return;
            _progressBarView.UpdateProgressbar(_progression.CurrentExperience, _progression.MaxUpgrade);
            _healthPanel.ChangeHealth(_player.CurrentHealth);
            _coinsView.ShowCoinsText(_wallet.Coins);

            if (_shield.HasShield) _shieldView.UpdateCoolDown(_shield.CurrentCoolDownCount, _shield.CoolDown);
        }
        
        private void UpdateHealth()
        {
            _healthPanel.UpdateHealth(_player.MaxHealth);
            _shieldView.transform.SetAsLastSibling();
        }
    }
}