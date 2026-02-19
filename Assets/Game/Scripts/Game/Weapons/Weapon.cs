using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class Weapon : MonoBehaviour
    {
        protected PoolManager _poolManager;
        protected WeaponStats _stats;

        public virtual void Construct(PoolManager poolManager) => _poolManager = poolManager;
        public virtual void Apply() { }
        public virtual void UpdateValues() { }

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