using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core.UI;
using ExtensionSystems;

namespace Game
{
    public class UpgradeWindowView : Window
    {
        private readonly Dictionary<Weapons, UpgradeButtonView> _buttonViews = new();
        private readonly List<IDisposable> _disposables = new();

        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _crystalText;

        private UpgradeWindowViewModel _viewModel;

        [field: SerializeField] public RectTransform ButtonsContainer { get; private set; }
        [field: SerializeField] public UpgradeButtonView UpgradeButtonViewTemplate { get; private set; }

        public void Bind(UpgradeWindowViewModel viewModel)
        {
            _viewModel = viewModel;

            _closeButton.onClick.AddListener(OnCloseWindow);
            _disposables.Add(_viewModel.Coins.Subscribe(RenderCoin));
            _disposables.Add(_viewModel.Crystals.Subscribe(RenderCrystals));
            _viewModel.OnButtonsChanged += RefreshButtons;
            RefreshButtons();
        }

        public void Unbind()
        {
            _disposables.ForEach(d => d.Dispose());
            _buttonViews.Values.ForEach(b => b.Unbind());
            _viewModel.OnButtonsChanged -= RefreshButtons;
            _closeButton.onClick.RemoveListener(OnCloseWindow);
            _disposables.Clear();
            _viewModel = null;
        }
        
        private void RefreshButtons()
        {
            foreach (KeyValuePair<Weapons, UpgradeButtonViewModel> button in _viewModel.Buttons)
            {
                if (!_buttonViews.ContainsKey(button.Key))
                    _buttonViews[button.Key] = Instantiate(UpgradeButtonViewTemplate, ButtonsContainer);
                
                _buttonViews[button.Key].Bind(button.Value);
            }
        }

        private void RenderCoin(int coins) => _coinsText.text = coins.ToString();
        private void RenderCrystals(int crystals) => _crystalText.text = crystals.ToString();
        private void OnCloseWindow() => _viewModel?.CloseCommand.Execute();
    }
}