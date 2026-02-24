using System.Collections.Generic;
using System.Linq;
using ExtensionSystems;
using Core.UI;

namespace Game
{
    public class UpgradeSystem
    {
        private readonly Dictionary<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> _weaponLevelUpsData = new();
        private readonly LevelUpInfo<PlayerLevelUpsData, SpartanStats> _playerLevelUpInfo;
        private readonly GameConfig _gameConfig;
        private readonly Factory _factory;
        private readonly Player _player;
        private readonly Wallet _wallet;

        public IReadOnlyDictionary<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> WeaponLevelUpsData => _weaponLevelUpsData;
        public LevelUpInfo<PlayerLevelUpsData, SpartanStats> PlayerLevelUpInfo => _playerLevelUpInfo;
        
        public UpgradeSystem(IReadOnlyList<WeaponLevelUpsData> weaponLevelUpsData, PlayerLevelUpsData playerLevelUpsData, Factory factory, Player player, GameConfig gameConfig, Wallet wallet)
        {
            weaponLevelUpsData.ForEach(d => _weaponLevelUpsData.Add(d.Name, new(d)));
            _playerLevelUpInfo = new(playerLevelUpsData);
            _factory = factory;
            _gameConfig = gameConfig;
            _player = player;
            _wallet = wallet;
        }
        
        public bool IsMaxUpgrades =>
            _playerLevelUpInfo.CurrentLevelUp >= _playerLevelUpInfo.CountLevelUps &&
            _weaponLevelUpsData.Values.All(w => w.CurrentLevelUp >= w.CountLevelUps);
        
        public void Init()
        {
            _playerLevelUpInfo.Reset();
            _weaponLevelUpsData.Values.ForEach(w => w.Reset());

            Upgrade(Weapons.Player);
            _gameConfig.StartWeapons.ForEach(w => Upgrade(w));
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
                if (!_player.Weapons.ContainsKey(name))
                {
                    Weapon weapon = _factory.CreateWeapon(_weaponLevelUpsData[name].LevelUpData.WeaponTemplate, _player.transform);
                    _player.AddWeapon(name, weapon);
                }

                _weaponLevelUpsData[name].LevelUp();
                _player.SetWeaponStats(name, _weaponLevelUpsData[name].Stats);
            }
            
            UIManager.GetScreen<HUD>().Refresh();
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