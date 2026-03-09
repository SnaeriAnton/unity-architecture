using System;
using UnityEngine;
using Contracts;
using Core.Pool;

namespace Game
{
    public class ProjectileView : MonoBehaviour, IPoolable
    {
        protected ProjectileModel _model;
        
        private Action _onDespawned;

        public int PoolID { get; private set; }

        public event Action<IEnemyTarget> OnHit;

        public virtual void Init(ProjectileModel model) => _model = model;
        
        public virtual void Disable()
        {
            _onDespawned.Invoke();
        }

        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        protected void InvokeOnHit(IEnemyTarget target) => OnHit?.Invoke(target);

        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            _onDespawned = onDespawned;
            gameObject.SetActive(true);
        }
    }
}