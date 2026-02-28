
using System;

namespace Shared
{
    public interface IPoolable
    {
        public int PoolID { get; }
        
        public void OnSpawned(int poolID, Action onDespawned);
        public void OnDespawned();
    }
}