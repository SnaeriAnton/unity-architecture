using System.Collections.Generic;
using Application;
using Domain;
using Presentation;

namespace Main
{
    public class UpgradeDefinitionsBuilder
    {
        private readonly  Dictionary<Weapons, Weapon> _weaponTemplates = new();
        private readonly List<WeaponUpgradeDefinition> _weaponLevelUpsWeaponData = new();

        public UpgradeDefinitionsBuilder(IReadOnlyList<WeaponLevelUpsData> weaponLevelUpsData,  PlayerLevelUpsData playerLevelUpsData)
        {   
            List<UpgradeDescription<SpartanStats>> playerLevelUps = new();
            playerLevelUpsData.LevelUps.ForEach(l => playerLevelUps.Add(new(l.Type, l.Price, l.Stats)));
            weaponLevelUpsData.ForEach(w => _weaponTemplates[w.Name] = w.WeaponTemplate);
            PlayerData = new(playerLevelUps, playerLevelUpsData.Name);
            
            foreach (WeaponLevelUpsData levelUps in weaponLevelUpsData)
            {
                List<UpgradeDescription<WeaponStats>> weaponLevelUps = new();
                levelUps.LevelUps.ForEach(l => weaponLevelUps.Add(new(l.Type, l.Price, l.Stats)));
                _weaponLevelUpsWeaponData.Add(new(weaponLevelUps, levelUps.Name));
            }
        }

        public IReadOnlyDictionary<Weapons, Weapon> WeaponTemplates => _weaponTemplates; 
        public IReadOnlyList<WeaponUpgradeDefinition> WeaponData => _weaponLevelUpsWeaponData;
        public PlayerUpgradeDefinition PlayerData { get; private set; }
    }
}