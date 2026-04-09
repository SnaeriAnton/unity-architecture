using System.Collections.Generic;
using System.Linq;
using ExtensionSystems;

namespace Game
{
    public class UpgradeModel : IUpgradeReadModel
    {
        private readonly Dictionary<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> _weaponLevelUpsData = new();
        private readonly LevelUpInfo<PlayerLevelUpsData, SpartanStats> _playerLevelUpInfo;
        
        public UpgradeModel(IReadOnlyList<WeaponLevelUpsData> weaponLevelUpsData, PlayerLevelUpsData playerLevelUpsData)
        {
            weaponLevelUpsData.ForEach(d => _weaponLevelUpsData.Add(d.Name, new(d)));
            _playerLevelUpInfo = new(playerLevelUpsData);
        }

        public IReadOnlyDictionary<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> WeaponLevelUpsData => _weaponLevelUpsData;
        public LevelUpInfo<PlayerLevelUpsData, SpartanStats> PlayerLevelUpInfo => _playerLevelUpInfo;
        public bool IsMaxUpgrades =>
            _playerLevelUpInfo.CurrentLevelUp >= _playerLevelUpInfo.CountLevelUps &&
            _weaponLevelUpsData.Values.All(w => w.CurrentLevelUp >= w.CountLevelUps);

        public Weapon GetWeaponTemplate(Weapons name) => _weaponLevelUpsData[name].LevelUpData.WeaponTemplate;
        
        public void Reset()
        {
            _playerLevelUpInfo.Reset();
            _weaponLevelUpsData.Values.ForEach(w => w.Reset());
        }

        public void LevelUp(Weapons name)
        {
            if (name == Weapons.Player)
                _playerLevelUpInfo.LevelUp();
            else
                _weaponLevelUpsData[name].LevelUp();
        }
        
        public (CurrencyType, int) GetPaymentInfo(Weapons name)
        {
            if (name == Weapons.Player)
            {
                LevelUpDescription<SpartanStats> nextPlayerStats = _playerLevelUpInfo.GetNextStats();   
                return new(nextPlayerStats.Type, nextPlayerStats.Price);
            }

            LevelUpDescription<WeaponStats> nextWeaponStats = _weaponLevelUpsData[name].GetNextStats();   
            return new(nextWeaponStats.Type, nextWeaponStats.Price);
        }
    }
}