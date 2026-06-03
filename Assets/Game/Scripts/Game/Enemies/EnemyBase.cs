using System;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class EnemyBase : MonoBehaviour, IPoolable
    {
        [SerializeField] protected EnemyEcsLink _link;

        protected GameApi _gameApi;
        protected PoolManager _poolManager;
        protected Transform _playerTransform;
        
        private Action _onDespawned;

        public float Health { get; protected set; }
        public int Damage { get; protected set; } = 0;
        public float AttackCooldown { get; protected set; } = 0;
        public float Speed { get; protected set; } = 0;
        public int PoolID { get; private set; }

        public virtual void Construct(Transform playerTransform, PoolManager poolManager, GameApi gameApi)
        {
            _playerTransform = playerTransform;
            _poolManager = poolManager;
            _gameApi = gameApi;
        }

        public virtual void SetupEcs(Entity entity, GameApi gameApi) { }
        
        public void TakeDamage(float damage)
        {
            EnemyEcsLink link = GetComponent<EnemyEcsLink>();

            if (!link.IsRegistered) return;

            _gameApi.RequestEnemyDamage(link.Entity, damage);
        }
        
        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            _onDespawned = onDespawned;
            gameObject.SetActive(true);
        }
    }
}