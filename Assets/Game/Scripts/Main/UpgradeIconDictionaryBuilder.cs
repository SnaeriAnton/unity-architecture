using System.Collections.Generic;
using UnityEngine;
using Domain;
using Presentation;

namespace Main
{
    public class UpgradeIconDictionaryBuilder
    {
        private readonly Dictionary<Weapons, Sprite> _upgradeIconDictionary = new();

        public UpgradeIconDictionaryBuilder(IReadOnlyList<WeaponLevelUpsData> weaponLevelUpsData, PlayerLevelUpsData playerLevelUpsData)
        {
            weaponLevelUpsData.ForEach(w => _upgradeIconDictionary[w.Name] = w.Icon);
            _upgradeIconDictionary[playerLevelUpsData.Name] = playerLevelUpsData.Icon;
        }

        public IReadOnlyDictionary<Weapons, Sprite> IconDictionary => _upgradeIconDictionary;
    }
}