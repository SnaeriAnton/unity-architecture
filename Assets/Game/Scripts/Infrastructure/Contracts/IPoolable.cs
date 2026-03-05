using System;

namespace Infrastructure
{
    public interface IPoolable
    {
        public int PoolID { get; }
        
        void OnSpawned(int poolID, Action onDespawned);
        void OnDespawned();
    }
}