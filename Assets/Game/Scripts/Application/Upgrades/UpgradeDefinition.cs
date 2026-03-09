
using System.Collections.Generic;
using Domain;

namespace Application
{
    public abstract class UpgradeDefinition<TStats> where TStats : struct
    {
        private readonly List<UpgradeDescription<TStats>> _upgrades;

        public UpgradeDefinition(IReadOnlyList<UpgradeDescription<TStats>> upgrades, Weapons name)
        {
            _upgrades = new(upgrades);
            Name = name;
        }

        public IReadOnlyList<UpgradeDescription<TStats>> Upgrades => _upgrades;
        public Weapons Name { get; }
        public int Count => _upgrades.Count;
    }
}