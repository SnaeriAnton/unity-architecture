using System;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class EnemyBase : MonoBehaviour, IPoolable
    {
        protected PoolManager _poolManager;
        protected Player _player;
        protected float _health;
        
        private Action _onDespawned;
        
        public int PoolID { get; private set; }
        
        private Action<EnemyBase> _obDiedCallBack;

        public virtual void Construct(Player player, Action<EnemyBase> obDiedCallBack, PoolManager poolManager)
        {
            _player = player;
            _obDiedCallBack = obDiedCallBack;
            _poolManager = poolManager;
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