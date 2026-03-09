using System;
using Contracts;
using UnityEngine;
using Object = UnityEngine.Object;
using Core.Pool;

namespace Game
{
    public class Factory
    {
        private readonly PoolManager _poolManager;
        private readonly ITarget _player;

        public Factory(PoolManager poolManager, ITarget player)
        {
            _poolManager = poolManager;
            _player = player;
        }

        public WeaponView CreateWeapon(WeaponView template)
        {
            WeaponView weaponView = Object.Instantiate(template, _player.Transform);
            weaponView.Construct(_poolManager);
            return weaponView;
        }

        public EnemyView SpawnEnemy(EnemyData data, Action<EnemyView> onDied, Vector3 pos)
        {
            EnemyView enemy = _poolManager.Spawn(data.EnemyTemplate, pos, Quaternion.identity);
            enemy.Construct(onDied, _poolManager);
            return enemy;
        }
    }
}