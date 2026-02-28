using System;
using UnityEngine;
using Shared;

namespace Enemies
{
    public abstract class EnemyBase : MonoBehaviour, IPoolable, IDamageable
    {
        protected IObjectPool _pool;
        protected ITarget _target;
        protected float _health;
        private Action _onDespawned;

        private Action<EnemyBase> _obDiedCallBack;

        public int PoolID { get; private set; }

        public virtual void Construct(ITarget target, Action<EnemyBase> obDiedCallBack, IObjectPool pool)
        {
            _target = target;
            _obDiedCallBack = obDiedCallBack;
            _pool = pool;
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