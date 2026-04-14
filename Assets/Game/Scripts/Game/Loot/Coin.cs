using Contracts;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Coin : MonoBehaviour, IPickup
    {
        private Coin.Pool _pool;

        [Inject]
        public void Construct(Coin.Pool pool) => _pool = pool;

        public void PickUp(IPickupReceiver receiver)
        {
            receiver.AddCoin();
            _pool.Despawn(this);
        }

        public class Pool : MonoMemoryPool<Coin>
        {
        }
    }
}