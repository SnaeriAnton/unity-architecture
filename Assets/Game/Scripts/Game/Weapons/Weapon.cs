using Contracts;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class Weapon : MonoBehaviour
    {
        protected PoolManager _poolManager;
        protected WeaponStats _stats;
        protected IGameLoop _loop;

        public virtual void Construct(IGameLoop loop, PoolManager poolManager)
        {
            _poolManager = poolManager;
            _loop = loop;
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