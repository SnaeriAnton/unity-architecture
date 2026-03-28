using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class UpgradeButtonView : MonoBehaviour
    {
        private readonly List<IDisposable> _disposables = new();
        
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Image _currencyIcon;
        [SerializeField] private Image _weaponIcon;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private GameObject _unlockPanel;
        [SerializeField] private GameObject _priceObject;

        private UpgradeButtonViewModel _viewModel;
        private UpgradeButtonViewData _data;
        
        public void Bind(UpgradeButtonViewModel viewModel)
        {
            Unbind();

            _viewModel = viewModel;

            _upgradeButton.onClick.AddListener(OnUpgrade);
            _disposables.Add(_viewModel.Data.Subscribe(RenderValues));
        }

        public void Unbind()
        {
            for (int i = 0; i < _disposables.Count; i++)
                _disposables[i].Dispose();

            _upgradeButton.onClick.RemoveListener(OnUpgrade);
            _disposables.Clear();
            _viewModel = null;
        }
        
        private void RenderValues(UpgradeButtonViewData data)
        {
            _data = data;
            _weaponIcon.sprite = data.WeaponIcon;
            _priceObject.SetActive(!data.IsMax);
            _levelText.enabled = data.IsUnlock;
            _unlockPanel.SetActive(!data.IsUnlock);
            _priceText.text = data.Price.ToString();
            _levelText.text = data.LevelText;
            _currencyIcon.sprite = data.CurrencyIcon;
        }
        
        private void OnUpgrade() => _viewModel?.UpgradeCommand.Execute();
    }
}