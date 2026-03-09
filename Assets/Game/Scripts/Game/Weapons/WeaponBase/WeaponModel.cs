using System;

namespace Game
{
    public abstract class WeaponModel
    {
        public WeaponStats Stats { get; private set; }
        
        public event Action OnStatsChanged; 
        public event Action OnReset;
        
        public virtual void SetStats(WeaponStats stats)
        {
            Stats = stats;
            OnStatsChanged?.Invoke();
        }

        public virtual void Reset()
        {
            Stats = default;
            OnReset?.Invoke();
        }
    }
}