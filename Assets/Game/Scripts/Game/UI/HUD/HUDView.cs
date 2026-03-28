using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class HUDView : Core.UI.Screen
    {
        private readonly List<IDisposable> _disposables = new();
        
        [SerializeField] private HealthPanel _healthPanel;
        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private ShieldView _shieldView;
        [SerializeField] private ProgressBarView _progressBarView;

        private HUDViewModel _viewModel;
        
        public void Bind(HUDViewModel viewModel)
        {
            _viewModel = viewModel;

            _disposables.Add(_viewModel.Coins.Subscribe(RenderCoin));
            _disposables.Add(_viewModel.CurrentExperience.Subscribe(RenderProgressFromViewModel));
            _disposables.Add(_viewModel.MaxUpgrade.Subscribe(RenderProgressFromViewModel));
            _disposables.Add(_viewModel.MaxHealth.Subscribe(RenderHealth));
            _disposables.Add(_viewModel.CurrentHealth.Subscribe(RenderHealthValues));
            _disposables.Add(_viewModel.CurrentCoolDownCount.Subscribe(RenderShieldFromViewModel));
            _disposables.Add(_viewModel.CoolDown.Subscribe(RenderShieldFromViewModel));
            _disposables.Add(_viewModel.HasShield.Subscribe(RenderShield));
            Reset();
        }

        public void Unbind()
        {
            _disposables.ForEach(d => d.Dispose());
            _disposables.Clear();
            _viewModel = null;
        }

        public override void Reset()
        {
            _healthPanel.Reset();
            _shieldView.Deactivate();
            RenderProgressFromViewModel(0);
        }
        
        private void RenderProgressFromViewModel(int currentExperience) => _progressBarView.UpdateProgressbar(_viewModel.CurrentExperience.Value, _viewModel.MaxUpgrade.Value);
        private void RenderHealthValues(int currentHealth) => _healthPanel.ChangeHealth(currentHealth);
        private void RenderCoin(int coins) => _coinsView.ShowCoinsText(coins);
        private void RenderShieldFromViewModel(float coolDown) => _shieldView.UpdateCoolDown(_viewModel.CurrentCoolDownCount.Value, _viewModel.CoolDown.Value);

        private void RenderShield(bool value)
        {
            if (!value) _shieldView.Deactivate();
        }
        
        private void RenderHealth(int maxHealth)
        {
            _healthPanel.UpdateHealth(maxHealth);
            _shieldView.transform.SetAsLastSibling();
        }
    }
}