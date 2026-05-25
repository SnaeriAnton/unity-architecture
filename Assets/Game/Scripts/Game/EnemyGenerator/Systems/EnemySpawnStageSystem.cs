namespace Game
{
    public class EnemySpawnStageSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EnemySpawnerController _spawner;
        private readonly CommandBuffer _commandBuffer;

        public EnemySpawnStageSystem(EcsWorld world, EnemySpawnerController spawner, CommandBuffer commandBuffer)
        {
            _world = world;
            _spawner = spawner;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity request in _world.Filter().With<EnemySpawnStageNextRequest>())
            {
                _spawner.LevelUp();
                _commandBuffer.DestroyEntity(request);
            }
        }
    }
}