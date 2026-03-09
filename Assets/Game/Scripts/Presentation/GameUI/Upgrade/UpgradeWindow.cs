using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Application;
using Domain;

namespace Presentation
{
    public class UpgradeWindow : Window
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _crystalText;
        [SerializeField] private TypeOfCurrency _typeOfCurrency;
        [SerializeField] private UpgradeButton _upgradeButtonTemplate;
        [SerializeField] private RectTransform _buttonsContainer;

        private readonly Dictionary<Weapons, UpgradeButton> _upgradeButtonsDictionary = new();

        private Dictionary<Weapons, Sprite> _upgradeIconDictionary = new();
        private ProgressionService _progression;
        private UpgradeSystem _upgrade;
        private Wallet _wallet;

        public void Construct(IReadOnlyDictionary<Weapons, Sprite> upgradeIconDictionary, ProgressionService progression, UpgradeSystem upgrade, Wallet wallet)
        {
            _upgradeIconDictionary = new(upgradeIconDictionary);
            _progression = progression;
            _upgrade = upgrade;
            _wallet = wallet;
            _closeButton.onClick.AddListener(Hide);
        }

        public override void Show()
        {
            base.Show();
            _crystalText.text = _wallet.Crystals.ToString();
            _coinsText.text = _wallet.Coins.ToString();

            Refresh();
        }

        public override void Hide()
        {
            base.Hide();
            _progression.UpgradeStats();
        }

        private void Refresh()
        {
            SetInfo(Weapons.Player,
                _upgrade.PlayerLevelUpInfo.GetNextStats().Type,
                _upgradeIconDictionary[Weapons.Player],
                _upgrade.PlayerLevelUpInfo.GetNextStats().Price,
                _upgrade.PlayerLevelUpInfo.CurrentLevelUp,
                _upgrade.PlayerLevelUpInfo.CountLevelUps);

            foreach (KeyValuePair<Weapons, LevelUpInfo<WeaponUpgradeDefinition, WeaponStats>> weapon in _upgrade.WeaponLevelUpsData)
            {
                UpgradeDescription<WeaponStats> description = weapon.Value.GetNextStats();
                SetInfo(weapon.Key, description.Type, _upgradeIconDictionary[weapon.Key], description.Price, weapon.Value.CurrentLevelUp, weapon.Value.CountLevelUps);
            }
        }

        private void SetInfo(Weapons name, CurrencyType currencyType, Sprite icon, int price, int currentLevel, int maxLevels)
        {
            if (!_upgradeButtonsDictionary.ContainsKey(name))
            {
                _upgradeButtonsDictionary[name] = Instantiate(_upgradeButtonTemplate, _buttonsContainer);
                _upgradeButtonsDictionary[name].Construct(OnClick, _typeOfCurrency, icon, name);
            }

            _upgradeButtonsDictionary[name].UpdateValues(currencyType, price, currentLevel, maxLevels);
        }

        private void OnClick(UpgradeButton upgradeButton)
        {
            if (!_upgrade.TryUpgrade(upgradeButton.Name)) return;

            _crystalText.text = _wallet.Crystals.ToString();
            _coinsText.text = _wallet.Coins.ToString();
            Refresh();
        }
    }
}