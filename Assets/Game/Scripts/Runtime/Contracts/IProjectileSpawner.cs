using Domain;
using UnityEngine;

namespace Runtime
{
    public interface IProjectileSpawner
    {
        public void SpawnSpear(Spear spearTemplate, Vector3 pos, Quaternion rot, WeaponStats stats, Vector2 dir);
        public void SpawnArrow(Arrow arrowTemplate, Vector3 pos, Quaternion rot, WeaponStats stats, Vector2 dir);
        public void SpawnAxe(Axe arrowTemplate, Vector3 pos, Quaternion rot, AxeStats stats, Vector2 dir);
    }
}
