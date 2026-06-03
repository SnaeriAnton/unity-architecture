
using Leopotam.EcsLite;

namespace Game
{
    public class EnemyDropSystem : IEcsRunSystem
    {
        private readonly EnemyDeathHandler _enemyDeathHandler;

        public EnemyDropSystem(EnemyDeathHandler enemyDeathHandler)
        {
            _enemyDeathHandler = enemyDeathHandler;
        }

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world
                .Filter<EnemyDropRequest>()
                .End();

            EcsPool<EnemyDropRequest> requestPool =
                world.GetPool<EnemyDropRequest>();

            foreach (int requestEntity in filter)
            {
                ref EnemyDropRequest request = ref requestPool.Get(requestEntity);

                _enemyDeathHandler.DropLoot(request.Position);
                
                world.DelEntity(requestEntity);
            }
        }
    }
}