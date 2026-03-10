using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Application;

namespace Presentation
{
    public class UpgradeWindow : Screen
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _crystalText;
        [SerializeField] private UpgradeButton _upgradeButtonTemplate;
        [SerializeField] private RectTransform _buttonsContainer;

        private readonly Dictionary<int, UpgradeButton> _upgradeButtonsDictionary = new();

        private Dictionary<int, Sprite> _currencyOfTypes;
        private ProgressionService _progression;
        private UpgradeSystem _upgrade;
        private WalletService _wallet;

        public void Construct(IReadOnlyDictionary<int, Sprite> currencyOfTypes, ProgressionService progression, UpgradeSystem upgrade, WalletService wallet)
        {
            _currencyOfTypes = new(currencyOfTypes);
            _progression = progression;
            _upgrade = upgrade;
            _wallet = wallet;
            _closeButton.onClick.AddListener(Hide);

            _upgrade.OnUpgrade += Refresh;
            _wallet.OnWalletChanged += ShowValues;
        }

        public override void Dispose()
        {
            base.Dispose();
            _upgrade.OnUpgrade -= Refresh;
            _wallet.OnWalletChanged -= ShowValues;
        }

        public override void Show()
        {
            base.Show();

            Refresh();
        }

        public override void Hide()
        {
            base.Hide();
            _progression.UpgradeStats();
        }

        private void Refresh()
        {
            List<UpgradeInfo> upgradeInfo = new(_upgrade.GetUpgradeInfo());

            foreach (UpgradeInfo weapon in upgradeInfo)
                SetInfo(weapon.NameID, weapon.CurrencyID, weapon.Icon, weapon.Price, weapon.CurrentLevel, weapon.MaxLevels);
        }

        private void SetInfo(int nameID, int currencyID, Sprite icon, int price, int currentLevel, int maxLevels)
        {
            if (!_upgradeButtonsDictionary.ContainsKey(nameID))
            {
                _upgradeButtonsDictionary[nameID] = Instantiate(_upgradeButtonTemplate, _buttonsContainer);
                _upgradeButtonsDictionary[nameID].Construct(OnClick, _currencyOfTypes[currencyID], icon, nameID);
            }

            _upgradeButtonsDictionary[nameID].UpdateValues(currencyID, price, currentLevel, maxLevels);
        }

        private void OnClick(UpgradeButton upgradeButton) => _upgrade.TryUpgrade(upgradeButton.NameID);

        private void ShowValues(int coins, int crystal)
        {
            _coinsText.text = coins.ToString();
            _coinsText.text = crystal.ToString();
        }
    }
}