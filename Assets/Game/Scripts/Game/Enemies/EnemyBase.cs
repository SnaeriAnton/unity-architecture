using System;
using Core.GSystem;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class EnemyBase : MonoBehaviour, IPoolable
    {
        protected float _health;
        
        private PoolManager _poolManager;
        private  Player _player;
        private Action _onDespawned;
        private Action<EnemyBase> _obDiedCallBack;
        
        protected Player Player => _player ??= G.Main.Resolve<Player>();
        protected PoolManager Pool => _poolManager ??= G.Main.Resolve<PoolManager>();
        public int PoolID { get; private set; }

        public virtual void Construct(Action<EnemyBase> obDiedCallBack) => _obDiedCallBack = obDiedCallBack;

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