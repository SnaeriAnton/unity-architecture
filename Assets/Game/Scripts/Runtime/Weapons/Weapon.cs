using Domain;
using UnityEngine;

namespace Runtime
{
    public abstract class Weapon : MonoBehaviour
    {
        protected WeaponStats _stats;
        protected IProjectileSpawner _spawner;

        public virtual void Construct(IProjectileSpawner poolManager) => _spawner = poolManager;
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