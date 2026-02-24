using Application;
using Domain;
using Runtime;
using UnityEngine;

namespace Presentation
{
    public class HUD : Screen, IHUDRefresher
    {
        [SerializeField] private HealthPanel _healthPanel;
        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private ShieldView _shieldView;
        [SerializeField] private ProgressBarView _progressBarView;

        private ProgressionService _progression;
        private Wallet _wallet;
        private Player _player;

        public void Construct(Player player, Wallet wallet, ProgressionService progression)
        {
            _progression = progression;
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
            _progressBarView.UpdateProgressbar(_progression.CurrentExperience, _progression.MaxUpgrade);
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