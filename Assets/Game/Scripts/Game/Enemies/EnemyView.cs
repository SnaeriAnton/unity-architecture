using System;
using Contracts;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class EnemyView : MonoBehaviour, IPoolable, IEnemyTarget
    {
        protected EnemyModel _model;
        protected PoolManager _poolManager;
        protected Action<EnemyView> _onDeadCallBack;

        private Action _onDespawned;

        public int PoolID { get; private set; }

        public virtual void Construct(Action<EnemyView> onDeadCallBack, PoolManager poolManager)
        {
            _onDeadCallBack = onDeadCallBack;
            _poolManager = poolManager;
        }

        public virtual void Init(EnemyModel model)
        {
            _model = model;
            _model.OnDiedCallBack += Die;
        }

        public void TakeDamage(float damage) => _model.TakeDamage(damage);

        protected void Die(EnemyModel model)
        {
            _model.OnDiedCallBack -= Die;
            _onDeadCallBack?.Invoke(this);
            gameObject.SetActive(false);
        }

        void IPoolable.OnDespawned() => gameObject.SetActive(false);
        protected virtual void OnTriggerEnter2D(Collider2D other) { }

        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            _onDespawned = onDespawned;
            gameObject.SetActive(true);
        }
    }
}