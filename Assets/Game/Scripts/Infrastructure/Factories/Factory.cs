using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Domain;
using Application;
using Runtime;

namespace Infrastructure
{
    public class Factory : IWeaponFactory, IProjectileSpawner
    {
        private readonly Dictionary<Weapons, Weapon> _weaponTemplates;
        private readonly PoolManager _poolManager;
        private readonly Player _player;

        public Factory(IReadOnlyDictionary<Weapons, Weapon> weaponTemplates, PoolManager poolManager, Player player)
        {
            _weaponTemplates = new(weaponTemplates);
            _poolManager = poolManager;
            _player = player;
        }

        public void CreateWeapon(Weapons name)
        {
            Weapon weapon = Object.Instantiate(_weaponTemplates[name], _player.transform);
            weapon.Construct(this);
            _player.AddWeapon(name, weapon);
        }

        public EnemyBase SpawnEnemy(EnemyBase prefab, Action<EnemyBase> onDied, Vector3 pos)
        {
            EnemyBase enemy = _poolManager.Spawn(prefab, pos, Quaternion.identity);
            enemy.Construct(_player, onDied, this);
            return enemy;
        }
        
        public void SpawnSpear(Spear spearTemplate, Vector3 pos, Quaternion rot, WeaponStats stats, Vector2 dir)
        {
            Spear spear = _poolManager.Spawn(spearTemplate, pos, rot);
            spear.Init(stats, dir);
        }

        public void SpawnArrow(Arrow arrowTemplate, Vector3 pos, Quaternion rot, WeaponStats stats, Vector2 dir)
        {
   
        }

        public void SpawnAxe(Axe arrowTemplate, Vector3 pos, Quaternion rot, AxeStats stats, Vector2 dir)
        {

        }
    }
}