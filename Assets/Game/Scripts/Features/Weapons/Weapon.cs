using UnityEngine;
using Shared;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        protected IObjectPool _pool;
        protected WeaponStats _stats;

        public virtual void Construct(IObjectPool poolManager) => _pool = poolManager;
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