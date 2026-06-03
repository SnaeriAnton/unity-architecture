using Leopotam.EcsLite;

namespace Game
{
    public class EnemySpawnStageSystem : IEcsRunSystem
    {
        private readonly EnemySpawnerController _spawnerController;

        public EnemySpawnStageSystem(EnemySpawnerController spawnerController)
        {
            _spawnerController = spawnerController;
        }

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<EnemySpawnStageNextRequest>().End();

            foreach (int request in filter)
            {
                _spawnerController.LevelUp();

                world.DelEntity(request);
            }
        }
    }
}