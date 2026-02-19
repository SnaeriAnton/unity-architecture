using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Core.Pool;

namespace Game
{
    public class Factory
    {
        private readonly PoolManager _poolManager;
        private readonly Player _player;

        public Factory(PoolManager poolManager, Player player)
        {
            _poolManager = poolManager;
            _player = player;
        }

        public Weapon CreateWeapon(Weapon template, Transform parent)
        {
            Weapon weapon = Object.Instantiate(template, parent);
            weapon.Construct(_poolManager);
            return weapon;
        }

        public EnemyBase SpawnEnemy(EnemyBase prefab, Action<EnemyBase> onDied, Vector3 pos)
        {
            EnemyBase enemy = _poolManager.Spawn(prefab, pos, Quaternion.identity);
            enemy.Construct(_player, onDied, _poolManager);
            return enemy;
        }
    }
}