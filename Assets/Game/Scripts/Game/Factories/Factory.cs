using UnityEngine;
using Object = UnityEngine.Object;
using Core.Pool;

namespace Game
{
    public class Factory
    {
        private readonly PoolManager _poolManager;
        private readonly Transform _playerTransform;
        private readonly GameApi _gameApi;

        public Factory(PoolManager poolManager, Transform playerTransform, GameApi gameApi)
        {
            _poolManager = poolManager;
            _playerTransform = playerTransform;
            _gameApi = gameApi;
        }

        public Weapon CreateWeapon(Weapon template, Transform parent)
        {
            Weapon weapon = Object.Instantiate(template, parent);
            weapon.Construct(_poolManager, _gameApi);
            return weapon;
        }

        public EnemyBase SpawnEnemy(EnemyBase prefab, Vector3 pos)
        {
            EnemyBase enemy = _poolManager.Spawn(prefab, pos, Quaternion.identity);
            enemy.Construct(_playerTransform, _poolManager, _gameApi);
            Entity enemyEntity = _gameApi.RegisterEnemy(enemy,enemy.transform, enemy.Speed, enemy.Health, enemy.Damage, enemy.AttackCooldown);

            enemy.GetComponent<EnemyEcsLink>().Bind(enemyEntity);
            enemy.SetupEcs(enemyEntity, _gameApi);
            return enemy;
        }
    }
}