using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class Weapon : MonoBehaviour
    {
        protected PoolManager _poolManager;
        protected WeaponStats _stats;
        protected ProjectileApi ProjectileApi;
        protected ProjectileWeaponApi ProjectileWeaponApi;
        protected WeaponHitboxApi WeaponHitboxApi;
        protected OrbitWeaponApi OrbitWeaponApi;

        public virtual void Construct(PoolManager poolManager, ProjectileApi projectileApi, ProjectileWeaponApi projectileWeaponApi, WeaponHitboxApi weaponHitboxApi, OrbitWeaponApi orbitWeaponApi)
        {
            _poolManager = poolManager;
            ProjectileApi = projectileApi;
            ProjectileWeaponApi = projectileWeaponApi;
            WeaponHitboxApi = weaponHitboxApi;
            OrbitWeaponApi = orbitWeaponApi;
        }

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