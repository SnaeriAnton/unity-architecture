using Zenject;

namespace Game
{
    public class VikingView : EnemyView
    {
        private VikingView.Pool _pool;

        [Inject]
        public void Construct(VikingView.Pool pool) => _pool = pool;
        public override void Die() => _pool.Despawn(this);

        public class Pool : MonoMemoryPool<VikingView>
        {
        }
    }
}