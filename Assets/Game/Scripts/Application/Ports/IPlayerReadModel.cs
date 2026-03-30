using System;

namespace Application
{
    public interface IPlayerReadModel
    {
        public int CurrentHealth { get; }
        public int MaxHealth { get; }
        
        public event Action OnHealthChanged;
        public event Action OnUpgradeStats;
    }
}
