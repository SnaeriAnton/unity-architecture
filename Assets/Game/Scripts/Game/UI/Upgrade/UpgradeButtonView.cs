using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        private UpgradeButtonViewData _data;
        
        public event Action<Weapons> OnClick;
        
        public void Initialize() => _upgradeButton.onClick.AddListener(Onclick);
        public void Dispose() => _upgradeButton.onClick.RemoveListener(Onclick);
        
        public void RenderValues(UpgradeButtonViewData data)
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
        
        private void Onclick() => OnClick?.Invoke(_data.Name);
    }
}