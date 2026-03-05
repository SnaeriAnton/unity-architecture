using System;
using UnityEngine;

namespace Infrastructure
{
    public abstract class EnemyBase : MonoBehaviour, IPoolable
    {
        protected PoolManager _spawner;
        protected Player _player;
        protected float _health;
        private Action _onDespawned;

        private Action<EnemyBase> _obDiedCallBack;

        public int PoolID { get; private set; }

        public virtual void Construct(Player player, Action<EnemyBase> obDiedCallBack, PoolManager spawner)
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