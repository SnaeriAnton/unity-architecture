using UnityEngine;
using Zenject;

namespace Game
{
    public class SamuraiView : EnemyView
    {
        private SamuraiView.Pool _pool;

        [Inject]
        public void Construct(SamuraiView.Pool pool) => _pool = pool;
        public override void SetPosition(Vector3 targetPosition, float speed, float dt) => transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * dt);
        public override void Die() => _pool.Despawn(this);
        
        public class Pool : MonoMemoryPool<SamuraiView>
        {
        }
    }
}