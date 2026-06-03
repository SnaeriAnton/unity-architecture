using Leopotam.EcsLite;

namespace Game
{
    public class EnemyDeathSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<EnemyTag>().Inc<EnemyDeadTag>().Inc<EnemyMonoRef>().Inc<EnemyViewRef>().Exc<EnemyDeathHandledTag>().End();

            EcsPool<EnemyDeathHandledTag> handledPool = world.GetPool<EnemyDeathHandledTag>();
            EcsPool<EnemyMonoRef> monoRefPool = world.GetPool<EnemyMonoRef>();
            EcsPool<EnemyViewRef> viewRefPool = world.GetPool<EnemyViewRef>();
            EcsPool<EnemyKilledRequest> killedRequestPool = world.GetPool<EnemyKilledRequest>();
            EcsPool<EnemyDropRequest> dropRequestPool = world.GetPool<EnemyDropRequest>();
            EcsPool<EnemyDespawnRequest> despawnRequestPool = world.GetPool<EnemyDespawnRequest>();

            foreach (int enemy in filter)
            {
                if (!handledPool.Has(enemy))
                    handledPool.Add(enemy);

                ref EnemyMonoRef monoRef = ref monoRefPool.Get(enemy);
                ref EnemyViewRef viewRef = ref viewRefPool.Get(enemy);

                int killedRequest = world.NewEntity();
                killedRequestPool.Add(killedRequest);

                int dropRequest = world.NewEntity();

                ref EnemyDropRequest dropData = ref dropRequestPool.Add(dropRequest);
                dropData.Position = viewRef.Transform.position;

                int despawnRequest = world.NewEntity();

                ref EnemyDespawnRequest despawnData = ref despawnRequestPool.Add(despawnRequest);
                despawnData.EnemyEntity = enemy;
                despawnData.Enemy = monoRef.Enemy;
            }
        }
    }
}