using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ExtensionSystems;

namespace Game
{
    public class UpgradeSystem
    {
        private readonly Dictionary<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> _weaponLevelUpsData = new();
        private readonly LevelUpInfo<PlayerLevelUpsData, SpartanStats> _playerLevelUpInfo;
        private readonly GameConfig _gameConfig;
        private readonly Factory _factory;
        private readonly WeaponApi _weaponApi;
        private readonly Wallet _wallet;
        private readonly PlayerApi  _playerApi;
        private readonly Transform _playerTransform;
        private readonly HudEcsApi _hudEcsApi;

        public UpgradeSystem(
            IReadOnlyList<WeaponLevelUpsData> weaponLevelUpsData, 
            PlayerLevelUpsData playerLevelUpsData, 
            Factory factory, 
            WeaponApi weaponApi, 
            GameConfig gameConfig, 
            Wallet wallet, 
            PlayerApi  playerApi,
            Transform playerTransform, 
            HudEcsApi hudEcsApi)
        {
            weaponLevelUpsData.ForEach(d => _weaponLevelUpsData.Add(d.Name, new(d)));
            _playerLevelUpInfo = new(playerLevelUpsData);
            _factory = factory;
            _gameConfig = gameConfig;
            _weaponApi = weaponApi;
            _wallet = wallet;
            _playerApi = playerApi;
            _playerTransform = playerTransform;
            _hudEcsApi = hudEcsApi;
        }

        public IReadOnlyDictionary<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> WeaponLevelUpsData => _weaponLevelUpsData;
        public LevelUpInfo<PlayerLevelUpsData, SpartanStats> PlayerLevelUpInfo => _playerLevelUpInfo;

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
                _playerApi.SetHealth(_playerLevelUpInfo.Stats);
            }
            else
            {
                if (!_weaponApi.Weapons.ContainsKey(name))
                {
                    Weapon weapon = _factory.CreateWeapon(_weaponLevelUpsData[name].LevelUpData.WeaponTemplate, _playerTransform);
                    _weaponApi.RequestAddWeapon(name, weapon);
                }

                _weaponLevelUpsData[name].LevelUp();
                _weaponApi.RequestSetStats(name, _weaponLevelUpsData[name].Stats);
            }

            _hudEcsApi.RequestRefresh();
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