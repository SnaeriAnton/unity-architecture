using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class UpgradeButton : MonoBehaviour
    {
        private const int LEVEL_UP_OFFSET = 1;
        
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Image _currencyIcon;
        [SerializeField] private Image _weaponIcon;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private GameObject _unlockPanel;
        [SerializeField] private GameObject _priceObject;

        private TypeOfCurrency _typeOfCurrency;

        public Weapons Name { get; private set; }
        public CurrencyType CurrencyType { get; private set; }
        public int Price { get; private set; }

        public void Construct(Action<UpgradeButton> onUpgrade, TypeOfCurrency typeOfCurrency, Sprite icon, Weapons name)
        {
            _typeOfCurrency = typeOfCurrency;
            _weaponIcon.sprite = icon;
            Name = name;
            _upgradeButton.onClick.AddListener(() => onUpgrade.Invoke(this));
        }

        public void UpdateValues(CurrencyType currencyType, int price, int level, int maxLevels)
        {
            CurrencyType = currencyType;

            if (level < 0)
            {
                _levelText.enabled = false;
                _unlockPanel.SetActive(true);   
            }
            else
            {
                _levelText.enabled = true;
                _unlockPanel.SetActive(false);
            }

            if (maxLevels == level + LEVEL_UP_OFFSET)
            {
                _priceObject.SetActive(false);
                _upgradeButton.interactable = false;
                _levelText.text = "MAX";
            }
            else
                _levelText.text = (level + 1).ToString();


            Price = price;
            _priceText.text = Price.ToString();
            _currencyIcon.sprite = _typeOfCurrency.GetSprite(currencyType);
        }
    }
}