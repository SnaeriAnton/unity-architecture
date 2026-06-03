using UnityEngine;
using Object = UnityEngine.Object;
using Core.Pool;

namespace Game
{
    public class Factory
    {
        private readonly PoolManager _poolManager;
        private readonly Transform _playerTransform;
        private readonly ProjectileApi _projectileApi;
        private readonly ProjectileWeaponApi _projectileWeaponApi;
        private readonly WeaponHitboxApi _weaponHitboxApi;
        private readonly OrbitWeaponApi _orbitWeaponApi;
        private readonly AxeApi _axeApi;
        private readonly LeoEnemyApi _leoEnemyApi;

        public Factory(PoolManager poolManager, Transform playerTransform, ProjectileApi projectileApi, ProjectileWeaponApi projectileWeaponApi, WeaponHitboxApi weaponHitboxApi, OrbitWeaponApi orbitWeaponApi, AxeApi axeApi, LeoEnemyApi leoEnemyApi)
        {
            _poolManager = poolManager;
            _playerTransform = playerTransform;
            _projectileApi = projectileApi;
            _projectileWeaponApi = projectileWeaponApi;
            _weaponHitboxApi = weaponHitboxApi;
            _orbitWeaponApi = orbitWeaponApi;
            _axeApi = axeApi;
            _leoEnemyApi = leoEnemyApi;
        }

        public Weapon CreateWeapon(Weapon template, Transform parent)
        {
            Weapon weapon = Object.Instantiate(template, parent);
            weapon.Construct(_poolManager, _projectileApi, _projectileWeaponApi, _weaponHitboxApi, _orbitWeaponApi);
            return weapon;
        }

        public EnemyBase SpawnEnemy(EnemyBase prefab, Vector3 pos)
        {
            EnemyBase enemy = _poolManager.Spawn(prefab, pos, Quaternion.identity);
            enemy.Construct(_playerTransform, _poolManager, _axeApi, _leoEnemyApi);
            int enemyEntity = _leoEnemyApi.RegisterEnemy(enemy, enemy.transform, enemy.Speed, enemy.Health, enemy.Damage, enemy.AttackCooldown);

            enemy.GetComponent<EnemyEcsLink>().Bind(enemyEntity);
            enemy.SetupEcs(enemyEntity);
            return enemy;
        }
    }
}