using System.Collections.Generic;
using Object = UnityEngine.Object;
using Core.UI;

namespace Game
{
    public class UpgradeWindowPresenter : ScreenPresenter
    {
        private const int LEVEL_UP_OFFSET = 1;

        private readonly Dictionary<Weapons, UpgradeButtonView> _upgradeButtonsDictionary = new();
        private readonly UpgradeWindowView _view;
        private readonly TypeOfCurrency _typeOfCurrency;
        private readonly IProgression _progression;
        private readonly IWallet _wallet;
        private readonly IUpgrade _upgrade;
        private readonly IUpgradeReadModel _upgradeModel;
        private readonly IProgressionReadModel _progressionModel;
        private readonly IUIService _uiService;

        public UpgradeWindowPresenter(UpgradeWindowView view, TypeOfCurrency typeOfCurrency, IProgression progression, IUpgrade upgrade, IUpgradeReadModel upgradeModel, IWallet wallet, IProgressionReadModel progressionModel, IUIService uiService)
        {
            _view = view;
            _typeOfCurrency = typeOfCurrency;
            _progression = progression;
            _upgrade = upgrade;
            _upgradeModel = upgradeModel;
            _wallet = wallet;
            _progressionModel = progressionModel;
            _uiService = uiService;
        }

        public void Initialize()
        {
            _view.Initialize();
            _view.OnClick += Hide;
            _progressionModel.OnLevelUp += _uiService.ShowWindow<UpgradeWindowView>;
        }

        public override void Dispose()
        {
            _view.Dispose();
            _view.OnClick -= Hide;
            _progressionModel.OnLevelUp -= _uiService.ShowWindow<UpgradeWindowView>;

            foreach (KeyValuePair<Weapons, UpgradeButtonView> upgrade in _upgradeButtonsDictionary)
            {
                upgrade.Value.OnClick -= OnClick;
                upgrade.Value.Dispose();
            }
        }
        
        public override void Show()
        {
            base.Show();
            UpdateValues();
            Refresh();
            _view.Show();
        }

        public override void Hide()
        {
            base.Hide();
            _view.Hide();
            _progression.UpgradeStats();
            _view.Hide();
        }

        private void UpdateValues() => _view.UpdateValues(_wallet.Coins, _wallet.Crystals);
        
        private void Refresh()
        {
            bool isMax = _upgradeModel.PlayerLevelUpInfo.CurrentLevelUp == _upgradeModel.PlayerLevelUpInfo.CountLevelUps + LEVEL_UP_OFFSET;
            string levelText = isMax ? "Max" : (_upgradeModel.PlayerLevelUpInfo.CurrentLevelUp + LEVEL_UP_OFFSET).ToString();

            SetInfo(new(
                _upgradeModel.PlayerLevelUpInfo.LevelUpData.Icon,
                _typeOfCurrency.GetSprite(_upgradeModel.PlayerLevelUpInfo.GetNextStats().Type),
                Weapons.Player,
                levelText,
                _upgradeModel.PlayerLevelUpInfo.CurrentLevelUp == _upgradeModel.PlayerLevelUpInfo.CountLevelUps + LEVEL_UP_OFFSET,
                _upgradeModel.PlayerLevelUpInfo.CurrentLevelUp >= 0,
                _upgradeModel.PlayerLevelUpInfo.GetNextStats().Price
            ));

            foreach (KeyValuePair<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> weapon in _upgradeModel.WeaponLevelUpsData)
            {
                LevelUpDescription<WeaponStats> description = weapon.Value.GetNextStats();
                isMax = weapon.Value.CurrentLevelUp == weapon.Value.CountLevelUps + LEVEL_UP_OFFSET;
                levelText = isMax ? "Max" : (weapon.Value.CurrentLevelUp + LEVEL_UP_OFFSET).ToString();

                SetInfo(new(
                    weapon.Value.LevelUpData.Icon,
                    _typeOfCurrency.GetSprite(description.Type),
                    weapon.Key,
                    levelText,
                    isMax,
                    weapon.Value.CurrentLevelUp >= 0,
                    description.Price
                ));
            }
        }

        private void SetInfo(UpgradeButtonViewData data)
        {
            if (!_upgradeButtonsDictionary.ContainsKey(data.Name))
            {
                _upgradeButtonsDictionary[data.Name] = Object.Instantiate(_view.UpgradeButtonViewTemplate, _view.ButtonsContainer);
                _upgradeButtonsDictionary[data.Name].OnClick += OnClick;
                _upgradeButtonsDictionary[data.Name].Initialize();
            }

            _upgradeButtonsDictionary[data.Name].RenderValues(data);
        }

        private void OnClick(Weapons name)
        {
            if (!_upgrade.TryUpgrade(name)) return;

            UpdateValues();
            Refresh();
        }
    }
}