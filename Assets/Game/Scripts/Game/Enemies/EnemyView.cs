using System;
using UnityEngine;

namespace Game
{
    public abstract class EnemyView : MonoBehaviour
    {
        public Vector3 Position => transform.position;

        public event Action<Collider2D> OnTrigger;

        public virtual void SetPosition(Vector3 targetPosition, float speed, float dt) { }
        public abstract void Die();

        protected virtual void OnTriggerEnter2D(Collider2D other) => OnTrigger?.Invoke(other);
    }
}