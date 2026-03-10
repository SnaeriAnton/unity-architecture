using System;
using System.Collections.Generic;
using Core.GSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core.UI;

namespace Game
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
        private ProgressionSystem _progression;
        private UpgradeSystem _upgrade;
        private Wallet _wallet;

        public override void Show()
        {
            _progression = G.Main.Resolve<ProgressionSystem>();
            _upgrade = G.Main.Resolve<UpgradeSystem>();
            _wallet = G.Main.Resolve<Wallet>();
            _closeButton.onClick.AddListener(Hide);
            
            base.Show();
            _crystalText.text = _wallet.Crystals.ToString();
            _coinsText.text = _wallet.Coins.ToString();

            Refresh();
        }

        public override void Hide()
        {
            base.Hide();
            _progression.UpgradeStats();
            _closeButton.onClick.RemoveAllListeners();
        }

        private void Refresh()
        {
            SetInfo(Weapons.Player, 
                _upgrade.PlayerLevelUpInfo.GetNextStats().Type, 
                _upgrade.PlayerLevelUpInfo.LevelUpData.Icon, 
                _upgrade.PlayerLevelUpInfo.GetNextStats().Price, 
                _upgrade.PlayerLevelUpInfo.CurrentLevelUp, 
                _upgrade.PlayerLevelUpInfo.CountLevelUps);
            
            foreach (KeyValuePair<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> weapon in _upgrade.WeaponLevelUpsData)
            {
                LevelUpDescription<WeaponStats> description = weapon.Value.GetNextStats();
                SetInfo(weapon.Key, description.Type, weapon.Value.LevelUpData.Icon, description.Price, weapon.Value.CurrentLevelUp, weapon.Value.CountLevelUps);
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