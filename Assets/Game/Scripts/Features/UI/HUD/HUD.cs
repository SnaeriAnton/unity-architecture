using UnityEngine;
using Shared;

namespace UI
{
    public class HUD : Screen
    {
        [SerializeField] private HealthPanel _healthPanel;
        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private ShieldView _shieldView;
        [SerializeField] private ProgressBarView _progressBarView;

        public void Construct()
        {
            GameEvents.Wallet.CoinsChanged += _coinsView.ShowCoinsText;
            GameEvents.Player.HealthChanged += _healthPanel.ChangeHealth;
            GameEvents.Player.ShieldReloading += _shieldView.UpdateCoolDown;
            GameEvents.Progression.ExpChanged += _progressBarView.UpdateProgressbar;
            GameEvents.Player.ChangeHealthCount += UpdateHealth;
        }

        public override void Dispose()
        {
            base.Dispose();
            GameEvents.Wallet.CoinsChanged -= _coinsView.ShowCoinsText;
            GameEvents.Player.HealthChanged -= _healthPanel.ChangeHealth;
            GameEvents.Player.ShieldReloading -= _shieldView.UpdateCoolDown;
            GameEvents.Progression.ExpChanged -= _progressBarView.UpdateProgressbar;
            GameEvents.Player.ChangeHealthCount -= UpdateHealth;
        }

        public override void Reset()
        {
            _shieldView.Deactivate();
            _healthPanel.Reset();
        }

        private void UpdateHealth(int maxHealth)
        {
            _healthPanel.UpdateHealth(maxHealth);
            _shieldView.transform.SetAsLastSibling();
        }
    }
}