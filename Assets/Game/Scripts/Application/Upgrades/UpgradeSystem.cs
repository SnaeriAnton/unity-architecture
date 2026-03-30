using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application
{
    public class UpgradeSystem : IUpgradeReadModel, IUpgradeCommands, IUpgradeState
    {
        private readonly Dictionary<Weapons, LevelUpInfo<WeaponUpgradeDefinition, WeaponStats>> _weaponLevelUpsData = new();
        private readonly LevelUpInfo<PlayerUpgradeDefinition, SpartanStats> _playerLevelUpInfo;
        private readonly IWallet _wallet;
        private readonly GameSettings _settings;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IPlayerSession _player;

        public UpgradeSystem(
            IReadOnlyList<WeaponUpgradeDefinition> weaponLevelUpsData,
            PlayerUpgradeDefinition playerLevelUpsData,
            GameSettings settings,
            IWallet wallet,
            IWeaponFactory weaponFactory,
            IPlayerSession player
        )
        {
            weaponLevelUpsData.ForEach(d => _weaponLevelUpsData.Add(d.Name, new(d)));
            _playerLevelUpInfo = new(playerLevelUpsData);
            _settings = settings;
            _wallet = wallet;
            _weaponFactory = weaponFactory;
            _player = player;
        }

        public bool IsMaxUpgrades =>
            _playerLevelUpInfo.CurrentLevelUp >= _playerLevelUpInfo.CountLevelUps &&
            _weaponLevelUpsData.Values.All(w => w.CurrentLevelUp >= w.CountLevelUps);

        public event Action OnUpgrade;

        public void Init()
        {
            _playerLevelUpInfo.Reset();
            _weaponLevelUpsData.Values.ForEach(w => w.Reset());

            Upgrade(Weapons.Player);
            _settings.StartWeapons.ForEach(w => Upgrade(w));
        }

        public bool TryUpgrade(Weapons name)
        {
            if (name == Weapons.Player)
            {
                if (TrySpend(_playerLevelUpInfo.GetNextStats()))
                {
                    Upgrade(name);
                    return true;
                }
            }
            else
            {
                if (TrySpend(_weaponLevelUpsData[name].GetNextStats()))
                {
                    Upgrade(name);
                    return true;
                }
            }

            return false;
        }

        public IReadOnlyList<UpgradeButtonViewData> GetUpgradeItems()
        {
            List<UpgradeButtonViewData> datas = new();

            datas.Add(new(
                Weapons.Player,
                _playerLevelUpInfo.GetNextStats().Type,
                _playerLevelUpInfo.CountLevelUps,
                _playerLevelUpInfo.GetNextStats().Price,
                _playerLevelUpInfo.CurrentLevelUp
            ));

            foreach (KeyValuePair<Weapons, LevelUpInfo<WeaponUpgradeDefinition, WeaponStats>> weapon in _weaponLevelUpsData)
            {
                datas.Add(new(
                    weapon.Key,
                    weapon.Value.GetNextStats().Type,
                    weapon.Value.CountLevelUps,
                    weapon.Value.GetNextStats().Price,
                    weapon.Value.CurrentLevelUp
                ));
            }

            return datas;
        }

        private void Upgrade(Weapons name)
        {
            if (name == Weapons.Player)
            {
                _playerLevelUpInfo.LevelUp();
                _player.SetPlayerStats(_playerLevelUpInfo.Stats);
            }
            else
            {
                if (!_player.HasWeapon(name))
                    _weaponFactory.CreateWeapon(name);

                _weaponLevelUpsData[name].LevelUp();
                _player.SetWeaponStats(name, _weaponLevelUpsData[name].Stats);
            }

            OnUpgrade?.Invoke();
        }

        private bool TrySpend<TStats>(UpgradeDescription<TStats> data) where TStats : struct
        {
            if (data.Type == CurrencyType.Coin)
                return _wallet.TrySpendCoins(data.Price);

            if (data.Type == CurrencyType.Crystal)
                return _wallet.TrySpendCrystals(data.Price);

            return false;
        }
    }
}