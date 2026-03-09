
using System;
using UnityEngine;

namespace Runtime
{
    public abstract class EnemyBase : MonoBehaviour, IPoolable
    {
        protected Player _player;
        protected IProjectileSpawner _spawner;
        protected float _health;
        
        private Action _onDespawned;
        private Action<EnemyBase> _obDiedCallBack;

        public int PoolID { get; private set; }

        public virtual void Construct(Player player, Action<EnemyBase> obDiedCallBack, IProjectileSpawner spawner)
        {
            _player = player;
            _obDiedCallBack = obDiedCallBack;
            _spawner = spawner;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                _obDiedCallBack.Invoke(this);
                Die();
            }
        }

        protected void Die() => gameObject.SetActive(false);

        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            _onDespawned = onDespawned;
            gameObject.SetActive(true);
        }
    }
}