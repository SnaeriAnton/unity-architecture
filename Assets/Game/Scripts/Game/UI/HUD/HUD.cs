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
        private Player _player;

        public void Construct(Player player, Wallet wallet, ProgressionSystem progressionSystem)
        {
            _progressionSystem = progressionSystem;
            _player = player;
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
            _healthPanel.ChangeHealth(_player.CurrentHealth);
            _coinsView.ShowCoinsText(_wallet.Coins);

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