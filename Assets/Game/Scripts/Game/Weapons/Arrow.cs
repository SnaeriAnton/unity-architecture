using System;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Arrow : MonoBehaviour, IPoolable
    {
        [SerializeField] private ProjectileEcsLink _projectileEcsLink;
        
        private Action _onDespawned;
        private GameApi _gameApi;

        public int PoolID { get; private set; }

        public void Init(GameApi gameApi, WeaponStats stats, Vector3 direction)
        {
            _gameApi = gameApi;
            
            Entity entity = _gameApi.RegisterProjectile(
                _projectileEcsLink.transform,
                _projectileEcsLink,
                _onDespawned,
                _projectileEcsLink.transform.position,
                direction,
                transform.rotation,
                stats.FlightSpeed,
                stats.LifeTime,
                stats.Damage);

            _projectileEcsLink.Bind(entity);
        }
        
        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out EnemyEcsLink enemyLink)) return;
            if (!enemyLink.IsRegistered) return;
            if (!_projectileEcsLink.IsRegistered) return;

            _gameApi.RequestProjectileHitEnemy(_projectileEcsLink.Entity, enemyLink.Entity);
        }
        
        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            _onDespawned = onDespawned;
            gameObject.SetActive(true);
        }
    }
}
