using System;
using UnityEngine;
using Shared;

namespace Enemies
{
    public class EnemiesFactory
    {
        private readonly IObjectPool _pool;
        private readonly ITarget _target;

        public EnemiesFactory(IObjectPool pool, ITarget target)
        {
            _pool = pool;
            _target = target;
        }
        
        public EnemyBase SpawnEnemy(EnemyBase prefab, Action<EnemyBase> onDied, Vector3 pos)
        {
            EnemyBase enemy = _pool.Spawn(prefab, pos, Quaternion.identity);
            enemy.Construct(_target, onDied, _pool);
            return enemy;
        }
    }
}
