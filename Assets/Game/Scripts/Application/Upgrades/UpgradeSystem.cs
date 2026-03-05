using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Infrastructure;

namespace Application
{
    public class UpgradeSystem
    {
        private readonly Dictionary<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> _weaponLevelUpsData = new();
        private readonly LevelUpInfo<PlayerLevelUpsData, SpartanStats> _playerLevelUpInfo;
        private readonly WalletService _wallet;
        private readonly GameConfig _config;
        private readonly Factory _weaponFactory;
        private readonly Player _player;

        public event Action OnUpgrade;

        public IReadOnlyDictionary<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> WeaponLevelUpsData => _weaponLevelUpsData;
        public LevelUpInfo<PlayerLevelUpsData, SpartanStats> PlayerLevelUpInfo => _playerLevelUpInfo;

        public UpgradeSystem(
            IReadOnlyList<WeaponLevelUpsData> weaponLevelUpsData,
            PlayerLevelUpsData playerLevelUpsData,
            GameConfig config,
            WalletService wallet,
            Factory weaponFactory,
            Player player
        )
        {
            weaponLevelUpsData.ForEach(d => _weaponLevelUpsData.Add(d.Name, new(d)));
            _playerLevelUpInfo = new(playerLevelUpsData);
            _config = config;
            _wallet = wallet;
            _weaponFactory = weaponFactory;
            _player = player;
        }
        
        public bool IsMaxUpgrades =>
            _playerLevelUpInfo.CurrentLevelUp >= _playerLevelUpInfo.CountLevelUps &&
            _weaponLevelUpsData.Values.All(w => w.CurrentLevelUp >= w.CountLevelUps);

        public void Init()
        {
            _playerLevelUpInfo.Reset();
            _weaponLevelUpsData.Values.ForEach(w => w.Reset());

            Upgrade(Weapons.Player);
            _config.StartWeapons.ForEach(w => Upgrade(w));
        }

        public IReadOnlyList<UpgradeInfo> GetUpgradeInfo()
        {
            List<UpgradeInfo> upgradeInfo = new();

            upgradeInfo.Add(new(
                (int)Weapons.Player,
                (int)_playerLevelUpInfo.GetNextStats().Type,
                _playerLevelUpInfo.LevelUpData.Icon,
                _playerLevelUpInfo.GetNextStats().Price,
                _playerLevelUpInfo.CurrentLevelUp,
                _playerLevelUpInfo.CountLevelUps));

            foreach (KeyValuePair<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> weapon in _weaponLevelUpsData)
            {
                LevelUpDescription<WeaponStats> description = weapon.Value.GetNextStats();
                upgradeInfo.Add(new((int)weapon.Key, (int)description.Type, weapon.Value.LevelUpData.Icon, description.Price, weapon.Value.CurrentLevelUp, weapon.Value.CountLevelUps));
            }

            return upgradeInfo;
        }

        public bool TryUpgrade(int nameID)
        {
            Weapons name = (Weapons)nameID;
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
                    _weaponFactory.CreateWeapon(_weaponLevelUpsData[name].LevelUpData.WeaponTemplate, name);

                _weaponLevelUpsData[name].LevelUp();
                _player.SetWeaponStats(name, _weaponLevelUpsData[name].Stats);
            }

            OnUpgrade?.Invoke();
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