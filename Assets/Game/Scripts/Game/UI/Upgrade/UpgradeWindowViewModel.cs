using System;
using System.Collections.Generic;
using ExtensionSystems;
using UniRx;

namespace Game
{
    public class UpgradeWindowViewModel : IDisposable
    {
        private const int LEVEL_UP_OFFSET = 1;

        private readonly ReactiveDictionary<Weapons, UpgradeButtonViewModel> _upgradeButtonsDictionary = new();
        private readonly TypeOfCurrency _typeOfCurrency;
        private readonly IProgression _progression;
        private readonly IWallet _wallet;
        private readonly IUpgrade _upgrade;
        private readonly IUpgradeReadModel _upgradeModel;
        private readonly Action _onClose;
        private readonly CompositeDisposable _disposable = new();

        public UpgradeWindowViewModel(TypeOfCurrency typeOfCurrency, IProgression progression, IUpgrade upgrade, IUpgradeReadModel upgradeModel, IWallet wallet, Action onClose)
        {
            _typeOfCurrency = typeOfCurrency;
            _progression = progression;
            _upgrade = upgrade;
            _upgradeModel = upgradeModel;
            _wallet = wallet;
            _onClose = onClose;

            CloseCommand.Subscribe(_ => Close()).AddTo(_disposable);
            _upgrade.OnUpgraded.Subscribe(_ => RefreshAll()).AddTo(_disposable);
            RefreshAll();
        }

        public IReadOnlyReactiveProperty<int> Coins => _wallet.Coins;
        public IReadOnlyReactiveProperty<int> Crystals => _wallet.Crystals;
        public IReadOnlyReactiveDictionary<Weapons, UpgradeButtonViewModel> Buttons => _upgradeButtonsDictionary;
        public ReactiveCommand CloseCommand { get; } = new();

        private void RefreshAll()
        {
            RefreshPlayerButton();

            foreach (KeyValuePair<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> weapon in _upgradeModel.WeaponLevelUpsData)
                RefreshWeaponButton(weapon.Key, weapon.Value);
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _upgradeButtonsDictionary.ForEach(d => d.Value.Dispose());
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
                    () => _upgrade.TryUpgrade(data.Name));

                _upgradeButtonsDictionary[data.Name] = buttonViewModel;
                return;
            }

            _upgradeButtonsDictionary[data.Name].Update(data);
        }

        private void Close()
        {
            _onClose?.Invoke();
            _progression.UpgradeStats();
        }
    }
}