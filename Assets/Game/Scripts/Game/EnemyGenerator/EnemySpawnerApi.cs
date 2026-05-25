namespace Game
{
    public class EnemySpawnerApi
    {
        private readonly EcsWorld _world;
        private readonly Entity _spawnerEntity;
        private readonly EnemySpawnerController _spawnerController;
        private readonly CommandBuffer _commandBuffer;

        public EnemySpawnerApi(EcsWorld world, Entity spawnerEntity, EnemySpawnerController spawnerController, CommandBuffer commandBuffer)
        {
            _world = world;
            _spawnerEntity = spawnerEntity;
            _spawnerController = spawnerController;
            _commandBuffer = commandBuffer;
        }

        public void RequestNextStage()
        {
            Entity request = _commandBuffer.CreateEntity();
            _commandBuffer.Add(request, new EnemySpawnStageNextRequest());
        }

        public void ResetTimer()
        {
            EnemySpawnTimer timer = _world.Get<EnemySpawnTimer>(_spawnerEntity);
            timer.TimeLeft = _spawnerController.CurrentSpawnInterval;
            _world.Set(_spawnerEntity, timer);
        }
    }
}