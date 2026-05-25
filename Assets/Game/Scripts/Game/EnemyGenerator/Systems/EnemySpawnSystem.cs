using System.Collections.Generic;

namespace Game
{
    public class EnemySpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly EnemySpawnerController _spawner;
        private readonly CommandBuffer _commandBuffer;

        public EnemySpawnSystem(EcsWorld world, EnemySpawnerController spawner, CommandBuffer commandBuffer)
        {
            _world = world;
            _spawner = spawner;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity request in _world.Filter().With<EnemySpawnRequest>())
            {
                _spawner.Spawn();
                _commandBuffer.DestroyEntity(request);
            }
        }
    }
}