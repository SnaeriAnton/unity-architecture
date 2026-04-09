using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core.UI;
using ExtensionSystems;
using R3;
using ObservableCollections;

namespace Game
{
    public class UpgradeWindowView : Window
    {
        private readonly Dictionary<Weapons, UpgradeButtonView> _buttonViews = new();

        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _crystalText;

        private CompositeDisposable _disposable = new();
        private UpgradeWindowViewModel _viewModel;

        [field: SerializeField] public RectTransform ButtonsContainer { get; private set; }
        [field: SerializeField] public UpgradeButtonView UpgradeButtonViewTemplate { get; private set; }

        public void Bind(UpgradeWindowViewModel viewModel)
        {
            Unbind();
            _viewModel = viewModel;
            _disposable = new();

            _closeButton.OnClickAsObservable().Subscribe(_ => _viewModel?.CloseCommand.Execute(Unit.Default)).AddTo(_disposable);
            _viewModel.Coins.Subscribe(RenderCoin).AddTo(_disposable);
            _viewModel.Crystals.Subscribe(RenderCrystals).AddTo(_disposable);
            _viewModel.Buttons.ForEach(v => CreateButton(v.Key, v.Value));
            _viewModel.Buttons.ObserveAdd().Subscribe(x => CreateButton(x.Value.Key, x.Value.Value)).AddTo(_disposable);
        }

        public void Unbind()
        {
            _buttonViews.Values.ForEach(b => b.Unbind());
            _disposable.Dispose();
            _viewModel = null;
        }

        private void CreateButton(Weapons key, UpgradeButtonViewModel vm)
        {
            if (_buttonViews.ContainsKey(key))
            {
                _buttonViews[key].Bind(vm);
                return;
            }

            UpgradeButtonView view = Instantiate(UpgradeButtonViewTemplate, ButtonsContainer);
            view.Bind(vm);
            _buttonViews[key] = view;
        }

        private void RenderCoin(int coins) => _coinsText.text = coins.ToString();
        private void RenderCrystals(int crystals) => _crystalText.text = crystals.ToString();
    }
}