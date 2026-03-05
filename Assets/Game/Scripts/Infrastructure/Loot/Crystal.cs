using System;
using UnityEngine;

namespace Infrastructure
{
    public class Crystal : MonoBehaviour, IPoolable
    {
        private Action _onDespawned;

        public int PoolID { get; private set; }
        
        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        public void PickUp()
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