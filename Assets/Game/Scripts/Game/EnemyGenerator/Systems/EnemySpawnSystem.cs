using Leopotam.EcsLite;

namespace Game
{
    public class EnemySpawnSystem : IEcsRunSystem
    {
        private readonly EnemySpawnerController _spawnerController;

        public EnemySpawnSystem(EnemySpawnerController spawnerController) => _spawnerController = spawnerController;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<EnemySpawnRequest>().End();

            foreach (int request in filter)
            {
                _spawnerController.Spawn();

                world.DelEntity(request);
            }
        }
    }
}