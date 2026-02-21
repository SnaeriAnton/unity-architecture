using Core.GSystem;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class Weapon : MonoBehaviour
    {
        private PoolManager _poolManager;
        
        protected WeaponStats _stats;

        protected PoolManager _pool => _poolManager ??= G.Main.Resolve<PoolManager>();

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