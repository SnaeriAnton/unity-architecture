using Contracts;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class Weapon : MonoBehaviour
    {
        protected PoolManager _poolManager;
        protected WeaponStats _stats;
        protected IUpdateStream _updateStream;

        public virtual void Construct(PoolManager poolManager, IUpdateStream updateStream)
        {
            _poolManager = poolManager;
            _updateStream = updateStream;
        }

        public virtual void Tick(float dt) { }
        public virtual void RefreshState() { }

        public virtual void SetStats(WeaponStats stats)
        {
            _stats = stats;
            gameObject.SetActive(true);
        }

        public virtual void Reset()
        {
            _stats = default;
            gameObject.SetActive(false);
        }
    }
}