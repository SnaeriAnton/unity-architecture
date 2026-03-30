using Domain;
using UnityEngine;

namespace Presentation
{
    public abstract class Weapon : MonoBehaviour
    {
        protected WeaponStats _stats;
        protected ProjectileSpawner _spawner;

        public virtual void Construct(ProjectileSpawner poolManager) => _spawner = poolManager;
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