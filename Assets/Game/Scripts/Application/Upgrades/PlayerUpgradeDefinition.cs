using System.Collections.Generic;
using Domain;

namespace Application
{
    public class PlayerUpgradeDefinition : UpgradeDefinition<SpartanStats>
    {
        public PlayerUpgradeDefinition(IReadOnlyList<UpgradeDescription<SpartanStats>> upgrades, Weapons name) 
            : base(upgrades, name) { }
    }
}