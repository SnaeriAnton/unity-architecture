using Contracts;
using UnityEngine;

namespace Game
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly Axe.Pool _pool;

        public ProjectileFactory(Axe.Pool pool) => _pool = pool;

        public Axe SpawnAxe(Vector3 position, Quaternion rotate)
        {
            Axe axe = _pool.Spawn();
            axe.transform.SetPositionAndRotation(position, rotate);
            return axe;
        }
    }
}