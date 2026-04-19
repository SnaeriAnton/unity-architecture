using Contracts;
using Core.Pool;
using UnityEngine;

namespace Game
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly IPoolService _pool;

        public ProjectileFactory(IPoolService pool) => _pool = pool;

        public Axe SpawnAxe(Axe template, Vector3 position, Quaternion rotate) => _pool.Spawn(template, position, rotate);
    }
}