using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Presentation
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

        public int NameID { get; private set; }
        public int CurrencyID { get; private set; }
        public int Price { get; private set; }

        public void Construct(Action<UpgradeButton> onUpgrade, Sprite currency, Sprite icon, int nameID)
        {
            _weaponIcon.sprite = icon;
            NameID = nameID;
            _upgradeButton.onClick.AddListener(() => onUpgrade.Invoke(this));
            _currencyIcon.sprite = currency;
        }

        public void UpdateValues(int currencyID, int price, int level, int maxLevels)
        {
            CurrencyID = currencyID;

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

        }
    }
}