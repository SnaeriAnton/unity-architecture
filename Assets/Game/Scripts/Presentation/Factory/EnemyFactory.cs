using System;
using UnityEngine;

namespace Presentation
{
    public class EnemyFactory
    {
        private readonly PoolManager _poolManager;
        private readonly Player _player;
        private readonly ProjectileSpawner _projectileSpawner;

        public EnemyFactory(PoolManager poolManager, Player player, ProjectileSpawner projectileSpawner)
        {
            _poolManager = poolManager;
            _player = player;
            _projectileSpawner = projectileSpawner;
        }

        public EnemyBase SpawnEnemy(EnemyBase prefab, Action<EnemyBase> onDied, Vector3 pos)
        {
            EnemyBase enemy = _poolManager.Spawn(prefab, pos, Quaternion.identity);
            enemy.Construct(_player, onDied, _projectileSpawner);
            return enemy;
        }
    }
}