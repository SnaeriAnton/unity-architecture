using System;
using Shared;
using UnityEngine;

namespace Loot
{
    public class LootEntity : MonoBehaviour, IPoolable, IPickup
    {
        private Action _onDespawned;
        
        public int PoolID { get; private set; }
        
        void IPoolable.OnDespawned() => gameObject.SetActive(false);
        
        public virtual void PickUp()
        {
            _onDespawned.Invoke();
            gameObject.SetActive(false);
        }
        
        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            _onDespawned = onDespawned;
            gameObject.SetActive(true);
        }
    }
}
