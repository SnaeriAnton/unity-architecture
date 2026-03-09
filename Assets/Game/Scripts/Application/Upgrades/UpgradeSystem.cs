
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application
{
    public class UpgradeSystem : IUpgradeService
    {
        private readonly Dictionary<Weapons, LevelUpInfo<WeaponUpgradeDefinition, WeaponStats>> _weaponLevelUpsData = new();
        private readonly LevelUpInfo<PlayerUpgradeDefinition, SpartanStats> _playerLevelUpInfo;
        private readonly Wallet _wallet;
        private readonly GameSettings _settings;
        private readonly IHUDRefresher _refresher;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IPlayerSession _player;
        
        public UpgradeSystem(
            IReadOnlyList<WeaponUpgradeDefinition> weaponLevelUpsData, 
            PlayerUpgradeDefinition playerLevelUpsData, 
            GameSettings settings, 
            Wallet wallet, 
            IHUDRefresher refresher, 
            IWeaponFactory weaponFactory, 
            IPlayerSession player
            )
        {
            weaponLevelUpsData.ForEach(d => _weaponLevelUpsData.Add(d.Name, new(d)));
            _playerLevelUpInfo = new(playerLevelUpsData);
            _settings = settings;
            _wallet = wallet;
            _refresher = refresher;
            _weaponFactory = weaponFactory;
            _player = player;
        }
        
        public IReadOnlyDictionary<Weapons, LevelUpInfo<WeaponUpgradeDefinition, WeaponStats>> WeaponLevelUpsData => _weaponLevelUpsData;
        public LevelUpInfo<PlayerUpgradeDefinition, SpartanStats> PlayerLevelUpInfo => _playerLevelUpInfo;
        public bool IsMaxUpgrades =>
            _playerLevelUpInfo.CurrentLevelUp >= _playerLevelUpInfo.CountLevelUps &&
            _weaponLevelUpsData.Values.All(w => w.CurrentLevelUp >= w.CountLevelUps);
        
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
            
            _refresher.Refresh();
        }

        private bool TrySpend<TStats>(UpgradeDescription<TStats> data) where TStats : struct
        {
            if (data.Type == CurrencyType.Coin)
                return _wallet.TrySpendCoin(data.Price);
            
            if (data.Type == CurrencyType.Crystal)
                return _wallet.TrySpendCrystal(data.Price);

            return false;
        }
    }
}