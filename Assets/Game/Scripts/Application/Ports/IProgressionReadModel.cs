using System;

namespace Application
{
    public interface IProgressionReadModel
    {
        public int MaxUpgrade { get; }
        public int CurrentExperience { get; }
        
        public event Action OnPickUpCrystal;
        public event Action OnUpgradeStats;
    }
}
