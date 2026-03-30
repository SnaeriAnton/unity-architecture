using UnityEngine;
using Domain;

namespace Presentation
{
    public class ProjectileSpawner
    {
        private readonly PoolManager _poolManager;

        public ProjectileSpawner(PoolManager poolManager) => _poolManager = poolManager;

        public void SpawnSpear(Spear spearTemplate, Vector3 pos, Quaternion rot, WeaponStats stats, Vector2 dir)
        {
            Spear spear = _poolManager.Spawn(spearTemplate, pos, rot);
            spear.Init(stats, dir);
        }

        public void SpawnArrow(Arrow arrowTemplate, Vector3 pos, Quaternion rot, WeaponStats stats, Vector2 dir)
        {
            Arrow spear = _poolManager.Spawn(arrowTemplate, pos, rot);
            spear.Init(stats, dir);
        }

        public void SpawnAxe(Axe arrowTemplate, Vector3 pos, Quaternion rot, AxeStats stats, Vector2 dir)
        {
            Axe spear = _poolManager.Spawn(arrowTemplate, pos, rot);
            spear.Init(stats, dir);
        }
    }
}