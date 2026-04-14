using Contracts;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Crystal : MonoBehaviour, IPickup
    {
        private Crystal.Pool _pool;

        [Inject]
        public void Construct(Crystal.Pool pool) => _pool = pool;

        public void PickUp(IPickupReceiver receiver)
        {
            receiver.AddExperience();
            _pool.Despawn(this);
        }

        public class Pool : MonoMemoryPool<Crystal>
        {
        }
    }
}