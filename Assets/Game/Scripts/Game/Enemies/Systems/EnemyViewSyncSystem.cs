using Leopotam.EcsLite;

namespace Game
{
    public class EnemyViewSyncSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<EnemyTag>().Inc<Position>().Inc<EnemyViewRef>().End();

            EcsPool<Position> positionPool = world.GetPool<Position>();
            EcsPool<EnemyViewRef> viewPool = world.GetPool<EnemyViewRef>();

            foreach (int enemy in filter)
            {
                ref Position position = ref positionPool.Get(enemy);
                ref EnemyViewRef viewRef = ref viewPool.Get(enemy);

                viewRef.Transform.position = position.Value;
            }
        }
    }
}