using System.Collections.Generic;
using System.Linq;
using ExtensionSystems;
using Shared;

namespace Upgrades
{
    public class UpgradeSystem : IUpgrade
    {
        private readonly Dictionary<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> _weaponLevelUpsData = new();
        private readonly LevelUpInfo<PlayerLevelUpsData, SpartanStats> _playerLevelUpInfo;
        private readonly List<Weapons> _startWeapons;
        private readonly IWeaponFactory _factory;
        private readonly IUpgradable _upgradable;
        private readonly IWallet _wallet;
        
        public UpgradeSystem(IReadOnlyList<WeaponLevelUpsData> weaponLevelUpsData, IReadOnlyList<Weapons> startWeapons, PlayerLevelUpsData playerLevelUpsData, IWeaponFactory factory, IUpgradable upgradable, IWallet wallet)
        {
            weaponLevelUpsData.ForEach(d => _weaponLevelUpsData.Add(d.Name, new(d)));
            _playerLevelUpInfo = new(playerLevelUpsData);
            _factory = factory;
            _upgradable = upgradable;
            _wallet = wallet;
            _startWeapons = new(startWeapons);
        }

        public IReadOnlyList<UpgradeInfo> GetUpgradeInfo()
        {
            List<UpgradeInfo> upgradeInfo = new();
            
            upgradeInfo.Add(new(
            Weapons.Player,
            _playerLevelUpInfo.GetNextStats().Type,
            _playerLevelUpInfo.LevelUpData.Icon,
            _playerLevelUpInfo.GetNextStats().Price,
            _playerLevelUpInfo.CurrentLevelUp,
            _playerLevelUpInfo.CountLevelUps));
            
            foreach (KeyValuePair<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> weapon in _weaponLevelUpsData)
            {
                LevelUpDescription<WeaponStats> description = weapon.Value.GetNextStats();
                upgradeInfo.Add( new(weapon.Key, description.Type, weapon.Value.LevelUpData.Icon, description.Price, weapon.Value.CurrentLevelUp, weapon.Value.CountLevelUps));
            }
            
            return upgradeInfo;
        }
        
        public bool IsMaxUpgrades =>
            _playerLevelUpInfo.CurrentLevelUp >= _playerLevelUpInfo.CountLevelUps &&
            _weaponLevelUpsData.Values.All(w => w.CurrentLevelUp >= w.CountLevelUps);
        
        public void Init()
        {
            _playerLevelUpInfo.Reset();
            _weaponLevelUpsData.Values.ForEach(w => w.Reset());

            Upgrade(Weapons.Player);
            _startWeapons.ForEach(w => Upgrade(w));
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

        private void Upgrade(Weapons name)
        {
            if (name == Weapons.Player)
            {
                _playerLevelUpInfo.LevelUp();
                _upgradable.SetPlayerStats(_playerLevelUpInfo.Stats);
            }
            else
            {
                if (!_upgradable.HasWeapon(name))
                    _factory.CreateWeapon(name, _upgradable.Transform);

                _weaponLevelUpsData[name].LevelUp();
                _upgradable.SetWeaponStats(name, _weaponLevelUpsData[name].Stats);
            }
        }

        private bool TrySpend<TStats>(LevelUpDescription<TStats> data) where TStats : struct
        {
            if (data.Type == CurrencyType.Coin)
                return _wallet.TrySpendCoin(data.Price);
            
            if (data.Type == CurrencyType.Crystal)
                return _wallet.TrySpendCrystal(data.Price);

            return false;
        }
    }
}