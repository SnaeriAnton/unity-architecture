using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class KnightView : EnemyView
    {
        private KnightView.Pool _pool;
        
        public event Action<Collider2D> OnTriggerExit;

        [Inject]
        public void Construct(KnightView.Pool pool) => _pool = pool;
        public override void SetPosition(Vector3 targetPosition, float speed, float dt) => transform.position =  Vector3.MoveTowards(transform.position, targetPosition, speed * dt);
        private void OnTriggerExit2D(Collider2D other) => OnTriggerExit?.Invoke(other);
        public override void Die() => _pool.Despawn(this);

        public class Pool : MonoMemoryPool<KnightView>
        {
        }
    }
}