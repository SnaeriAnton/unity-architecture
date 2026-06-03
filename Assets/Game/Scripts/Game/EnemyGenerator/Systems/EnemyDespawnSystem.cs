using Leopotam.EcsLite;

namespace Game
{
    public class EnemyDespawnSystem : IEcsRunSystem
    {
        private readonly EnemyDeathHandler _enemyDeathHandler;

        public EnemyDespawnSystem(EnemyDeathHandler enemyDeathHandler)
        {
            _enemyDeathHandler = enemyDeathHandler;
        }

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world
                .Filter<EnemyDespawnRequest>()
                .End();

            EcsPool<EnemyDespawnRequest> requestPool =
                world.GetPool<EnemyDespawnRequest>();

            foreach (int requestEntity in filter)
            {
                ref EnemyDespawnRequest request = ref requestPool.Get(requestEntity);

                EnemyEcsLink link = request.Enemy.GetComponent<EnemyEcsLink>();

                if (link != null)
                    link.Clear();

                _enemyDeathHandler.Handle(request.Enemy);

                if (world.GetPool<EnemyTag>().Has(request.EnemyEntity))
                    world.DelEntity(request.EnemyEntity);

                world.DelEntity(requestEntity);
            }
        }
    }
}