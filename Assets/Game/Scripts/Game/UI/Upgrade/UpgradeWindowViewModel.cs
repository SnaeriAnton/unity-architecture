using System;
using System.Collections.Generic;
using Core.MVVM;
using Core.UI;

namespace Game
{
    public class UpgradeWindowViewModel : ViewModelBase
    {
        private const int LEVEL_UP_OFFSET = 1;

        private readonly Dictionary<Weapons, UpgradeButtonViewModel> _upgradeButtonsDictionary = new();
        private readonly TypeOfCurrency _typeOfCurrency;
        private readonly IProgression _progression;
        private readonly IWallet _wallet;
        private readonly IUpgrade _upgrade;
        private readonly IUpgradeReadModel _upgradeModel;
        private readonly Action _onClose;

        private readonly ObservableProperty<int> _coins;
        private readonly ObservableProperty<int> _crystals;
        
        public UpgradeWindowViewModel(TypeOfCurrency typeOfCurrency, IProgression progression, IUpgrade upgrade, IUpgradeReadModel upgradeModel, IWallet wallet, Action onClose)
        {
            _typeOfCurrency = typeOfCurrency;
            _progression = progression;
            _upgrade = upgrade;
            _upgradeModel = upgradeModel;
            _wallet = wallet;
            _onClose = onClose;

            _coins = new(_wallet.Coins);
            _crystals = new(_wallet.Crystals);

            CloseCommand = new(Close);

            _upgrade.OnUpgraded += RefreshAll;
            RefreshAll();
        }
        
        public IReadOnlyObservableProperty<int> Coins => _coins;
        public IReadOnlyObservableProperty<int> Crystals => _crystals;
        public IReadOnlyDictionary<Weapons, UpgradeButtonViewModel> Buttons => _upgradeButtonsDictionary;
        public Command CloseCommand { get; }

        public event Action OnButtonsChanged;
        
        private  void RefreshAll()
        {
            _coins.Set(_wallet.Coins);
            _crystals.Set(_wallet.Crystals);

            RefreshPlayerButton();

            foreach (KeyValuePair<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> weapon in _upgradeModel.WeaponLevelUpsData)
                RefreshWeaponButton(weapon.Key, weapon.Value);
            
            OnButtonsChanged?.Invoke();
        }

        public override void Dispose()
        {
            _upgrade.OnUpgraded -= RefreshAll;
            base.Dispose();
        }

        private void RefreshPlayerButton()
        {
            bool isMax = _upgradeModel.PlayerLevelUpInfo.CurrentLevelUp ==
                         _upgradeModel.PlayerLevelUpInfo.CountLevelUps + LEVEL_UP_OFFSET;

            string levelText = isMax
                ? "Max"
                : (_upgradeModel.PlayerLevelUpInfo.CurrentLevelUp + LEVEL_UP_OFFSET).ToString();

            UpgradeButtonViewData data = new UpgradeButtonViewData(
                _upgradeModel.PlayerLevelUpInfo.LevelUpData.Icon,
                _typeOfCurrency.GetSprite(_upgradeModel.PlayerLevelUpInfo.GetNextStats().Type),
                Weapons.Player,
                levelText,
                isMax,
                _upgradeModel.PlayerLevelUpInfo.CurrentLevelUp >= 0,
                _upgradeModel.PlayerLevelUpInfo.GetNextStats().Price
            );

            SetInfo(data);
        }

        private void RefreshWeaponButton(Weapons weaponName, LevelUpInfo<WeaponLevelUpsData, WeaponStats> weapon)
        {
            var description = weapon.GetNextStats();
            bool isMax = weapon.CurrentLevelUp == weapon.CountLevelUps + LEVEL_UP_OFFSET;
            string levelText = isMax ? "Max" : (weapon.CurrentLevelUp + LEVEL_UP_OFFSET).ToString();

            UpgradeButtonViewData data = new UpgradeButtonViewData(
                weapon.LevelUpData.Icon,
                _typeOfCurrency.GetSprite(description.Type),
                weaponName,
                levelText,
                isMax,
                weapon.CurrentLevelUp >= 0,
                description.Price
            );

            SetInfo(data);
        }

        private void SetInfo(UpgradeButtonViewData data)
        {
            if (!_upgradeButtonsDictionary.ContainsKey(data.Name))
            {
                UpgradeButtonViewModel buttonViewModel = new(
                    data,
                    () => OnUpgradeClicked(data.Name));

                _upgradeButtonsDictionary[data.Name] = buttonViewModel;
            }

            _upgradeButtonsDictionary[data.Name].Update(data);
        }

        private void Close()
        {
            _onClose?.Invoke();
            _progression.UpgradeStats();
        }
        
        private void OnUpgradeClicked(Weapons name)
        {
            if (!_upgrade.TryUpgrade(name))
                return;

            RefreshAll();
        }
    }
}