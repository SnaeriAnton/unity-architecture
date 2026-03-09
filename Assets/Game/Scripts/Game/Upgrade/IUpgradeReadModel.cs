using System.Collections.Generic;

namespace Game
{
    public interface IUpgradeReadModel
    {
        public IReadOnlyDictionary<Weapons, LevelUpInfo<WeaponLevelUpsData, WeaponStats>> WeaponLevelUpsData { get; }
        public LevelUpInfo<PlayerLevelUpsData, SpartanStats> PlayerLevelUpInfo { get; }
        public bool IsMaxUpgrades { get; }
    }
}