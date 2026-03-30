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
        private IProgressionCommands _progressionCommands;
        private IUpgradeCommands _upgradeCommands;
        private IUpgradeReadModel _upgrade;
        private IWalletReadModel _wallet;

        public void Construct(IReadOnlyDictionary<Weapons, Sprite> upgradeIconDictionary, IProgressionCommands progressionCommands, IUpgradeReadModel upgrade, IUpgradeCommands upgradeCommands, IWalletReadModel wallet)
        {
            _upgradeIconDictionary = new(upgradeIconDictionary);
            _progressionCommands = progressionCommands;
            _upgrade = upgrade;
            _upgradeCommands = upgradeCommands;
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
            _progressionCommands.UpgradeStats();
        }

        private void Refresh()
        {
            List<UpgradeButtonViewData> datas = new(_upgrade.GetUpgradeItems());

            foreach (UpgradeButtonViewData data in datas)
            {
                SetInfo(
                    data.Name,
                    data.Type,
                    _upgradeIconDictionary[data.Name],
                    data.Price,
                    data.CurrentLevel,
                    data.CountLevels);
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
            if (!_upgradeCommands.TryUpgrade(upgradeButton.Name)) return;

            _crystalText.text = _wallet.Crystals.ToString();
            _coinsText.text = _wallet.Coins.ToString();
            Refresh();
        }
    }
}