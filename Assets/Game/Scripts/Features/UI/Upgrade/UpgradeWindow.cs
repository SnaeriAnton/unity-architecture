using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Shared;

namespace UI
{
    public class UpgradeWindow : Screen
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _crystalText;
        [SerializeField] private TypeOfCurrency _typeOfCurrency;
        [SerializeField] private UpgradeButton _upgradeButtonTemplate;
        [SerializeField] private RectTransform _buttonsContainer;

        private readonly Dictionary<Weapons, UpgradeButton> _upgradeButtonsDictionary = new();
        private IUpgrade _upgradeStates;

        public void Construct(IUpgrade upgradeStates)
        {
            _upgradeStates = upgradeStates;
            _closeButton.onClick.AddListener(Hide);

            GameEvents.Wallet.CoinsChanged += SetCoin;
            GameEvents.Wallet.CrystalsChanged += SetCrystal;
        }

        public override void Dispose()
        {
            base.Dispose();
            GameEvents.Wallet.CoinsChanged -= SetCoin;
            GameEvents.Wallet.CrystalsChanged -= SetCrystal;
        }

        public override void Show()
        {
            base.Show();
            Refresh();
        }

        public override void Hide()
        {
            base.Hide();
            GameEvents.Progression.RaiseUpgradeCompleted();
        }

        private void Refresh()
        {
            List<UpgradeInfo> upgradeInfo = new(_upgradeStates.GetUpgradeInfo());

            foreach (UpgradeInfo weapon in upgradeInfo)
                SetInfo(weapon.Name, weapon.CurrencyType, weapon.Icon, weapon.Price, weapon.CurrentLevel, weapon.MaxLevels);
        }

        private void SetInfo(Weapons name, CurrencyType currencyType, Sprite icon, int price, int currentLevel, int maxLevels)
        {
            if (!_upgradeButtonsDictionary.ContainsKey(name))
            {
                _upgradeButtonsDictionary[name] = Object.Instantiate(_upgradeButtonTemplate, _buttonsContainer);
                _upgradeButtonsDictionary[name].Construct(OnClick, _typeOfCurrency, icon, name);
            }

            _upgradeButtonsDictionary[name].UpdateValues(currencyType, price, currentLevel, maxLevels);
        }

        private void OnClick(UpgradeButton upgradeButton)
        {
            if (!_upgradeStates.TryUpgrade(upgradeButton.Name)) return;

            Refresh();
        }

        private void SetCrystal(int crystal) => _crystalText.text = crystal.ToString();
        private void SetCoin(int coin) => _coinsText.text = coin.ToString();
    }
}