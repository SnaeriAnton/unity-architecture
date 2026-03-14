using UnityEngine;

namespace Game
{
    public class HUDView : Core.UI.Screen
    {
        [SerializeField] private HealthPanel _healthPanel;
        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private ShieldView _shieldView;
        [SerializeField] private ProgressBarView _progressBarView;

        public void RenderHealthValues(int currentHealth) => _healthPanel.ChangeHealth(currentHealth);
        public void RenderCoin(int coins) => _coinsView.ShowCoinsText(coins);
        public void RenderProgress(int currentExperience, int maxUpgrade) => _progressBarView.UpdateProgressbar(currentExperience, maxUpgrade);
        public void RenderShieldValues(float coolDown, float maxCoolDown) => _shieldView.UpdateCoolDown(coolDown, maxCoolDown);

        public void RenderHealth(int maxHealth)
        {
            _healthPanel.UpdateHealth(maxHealth);
            _shieldView.transform.SetAsLastSibling();
        }

        public void Reset()
        {
            _healthPanel.Reset();
            _shieldView.Deactivate();
        }
    }
}