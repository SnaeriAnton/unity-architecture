using System;
using Core.GSystem;
using UnityEngine;
using Object = UnityEngine.Object;
using Core.Pool;

namespace Game
{
    public class Factory
    {
        private PoolManager _poolManager;
        
        private PoolManager Pool => _poolManager ??= G.Main.Resolve<PoolManager>();

        public Weapon CreateWeapon(Weapon template, Transform parent)
        {
            Weapon weapon = Object.Instantiate(template, parent);
            return weapon;
        }

        public EnemyBase SpawnEnemy(EnemyBase prefab, Action<EnemyBase> onDied, Vector3 pos)
        {
            EnemyBase enemy = Pool.Spawn(prefab, pos, Quaternion.identity);
            enemy.Construct(onDied);
            return enemy;
        }
    }
}