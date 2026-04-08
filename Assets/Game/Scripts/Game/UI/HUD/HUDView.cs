using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game
{
    public class HUDView : Core.UI.Screen
    {
        private readonly CompositeDisposable _disposable = new();
        
        [SerializeField] private HealthPanel _healthPanel;
        [SerializeField] private CoinsView _coinsView;
        [SerializeField] private ShieldView _shieldView;
        [SerializeField] private ProgressBarView _progressBarView;

        private HUDViewModel _viewModel;
        
        public void Bind(HUDViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.Coins.Subscribe(RenderCoin).AddTo(_disposable);
            _viewModel.CurrentExperience.Subscribe(RenderProgressFromViewModel).AddTo(_disposable);
            _viewModel.MaxUpgrade.Subscribe(RenderProgressFromViewModel).AddTo(_disposable);
            _viewModel.MaxHealth.Subscribe(RenderHealth).AddTo(_disposable);
            _viewModel.CurrentHealth.Subscribe(RenderHealthValues).AddTo(_disposable);
            _viewModel.CurrentCoolDownCount.Subscribe(RenderShieldFromViewModel).AddTo(_disposable);
            _viewModel.CoolDown.Subscribe(RenderShieldFromViewModel).AddTo(_disposable);
            _viewModel.HasShield.Subscribe(RenderShield).AddTo(_disposable);
            
            Reset();
        }

        public void Unbind()
        {
            _disposable.Dispose();
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
        private void RenderShieldFromViewModel(int coolDown) => _shieldView.UpdateCoolDown(_viewModel.CurrentCoolDownCount.Value, _viewModel.CoolDown.Value);

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