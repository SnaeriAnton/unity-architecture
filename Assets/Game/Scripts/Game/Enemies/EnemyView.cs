using System;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class EnemyView : MonoBehaviour, IPoolable
    {
        public Vector3 Position => transform.position;
        public int PoolID { get; private set; }

        public event Action<Collider2D> OnTrigger;

        public virtual void SetPosition(Vector3 targetPosition, float speed, float dt) { }
        public void Die() => gameObject.SetActive(false);

        protected virtual void OnTriggerEnter2D(Collider2D other) => OnTrigger?.Invoke(other);

        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            gameObject.SetActive(true);
        }
    }
}