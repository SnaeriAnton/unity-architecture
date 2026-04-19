using System;
using Contracts;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Coin : MonoBehaviour, IPoolable, IPickup
    {
        private Action _onDespawned;
        
        public int PoolID { get; private set; }
        
        void IPoolable.OnDespawned() => gameObject.SetActive(false);
        
        public void PickUp()
        {
            _onDespawned.Invoke();
            gameObject.SetActive(false);
        }
        
        public void PickUp(IPickupReceiver receiver)
        {
            receiver.AddCoin();
            PickUp();
        }
        
        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            _onDespawned = onDespawned;
            gameObject.SetActive(true);
        }
    }
}
