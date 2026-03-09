using System.Collections.Generic;
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
        private IProgression _progression;
        private IWallet _wallet;
        private IUpgrade _upgrade;
        private IUpgradeReadModel _upgradeModel;

        public void Construct(IProgression progression, IUpgrade upgrade, IUpgradeReadModel upgradeModel, IWallet wallet)
        {
            _progression = progression;
            _upgrade = upgrade;
            _upgradeModel = upgradeModel;
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
                _upgradeModel.PlayerLevelUpInfo.GetNextStats().Type,
                _upgradeModel.PlayerLevelUpInfo.LevelUpData.Icon,
                _upgradeModel.PlayerLevelUpInfo.GetNextStats().Price,
                _upgradeModel.PlayerLevelUpInfo.CurrentLevelUp,
                _upgradeModel.PlayerLevelUpInfo.CountLevelUps);

            foreach (KeyValuePair<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> weapon in _upgradeModel.WeaponLevelUpsData)
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