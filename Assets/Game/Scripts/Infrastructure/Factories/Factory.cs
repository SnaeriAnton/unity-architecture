using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Domain;

namespace Infrastructure
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

        public void CreateWeapon(Weapon weapon, Weapons name)
        {
            Weapon newWeapon = Object.Instantiate(weapon, _player.transform);
            newWeapon.Construct(_poolManager);
            _player.AddWeapon(name, newWeapon);
        }

        public EnemyBase SpawnEnemy(EnemyBase prefab, Action<EnemyBase> onDied, Vector3 pos)
        {
            EnemyBase enemy = _poolManager.Spawn(prefab, pos, Quaternion.identity);
            enemy.Construct(_player, onDied, _poolManager);
            return enemy;
        }
    }
}