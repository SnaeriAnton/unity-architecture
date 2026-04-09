using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;

namespace Game
{
    public class UpgradeButtonView : MonoBehaviour
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Image _currencyIcon;
        [SerializeField] private Image _weaponIcon;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private GameObject _unlockPanel;
        [SerializeField] private GameObject _priceObject;

        private CompositeDisposable _disposable = new();
        private UpgradeButtonViewModel _viewModel;

        public void Bind(UpgradeButtonViewModel viewModel)
        {
            Unbind();

            _viewModel = viewModel;

            _disposable = new();
            _upgradeButton.OnClickAsObservable().Subscribe(_ => _viewModel?.UpgradeCommand.Execute(Unit.Default)).AddTo(_disposable);
            _viewModel.Data.Subscribe(RenderValues).AddTo(_disposable);
        }

        public void Unbind()
        {
            _disposable.Dispose();
            _viewModel = null;
        }

        private void RenderValues(UpgradeButtonViewData data)
        {
            _weaponIcon.sprite = data.WeaponIcon;
            _priceObject.SetActive(!data.IsMax);
            _levelText.enabled = data.IsUnlock;
            _unlockPanel.SetActive(!data.IsUnlock);
            _priceText.text = data.Price.ToString();
            _levelText.text = data.LevelText;
            _currencyIcon.sprite = data.CurrencyIcon;
        }
    }
}