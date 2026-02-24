using System.Collections.Generic;
using Domain;

namespace Application
{
    public class WeaponUpgradeDefinition : UpgradeDefinition<WeaponStats>
    {
        public WeaponUpgradeDefinition(IReadOnlyList<UpgradeDescription<WeaponStats>> upgrades, Weapons name) 
            : base(upgrades, name) { }
    }
}