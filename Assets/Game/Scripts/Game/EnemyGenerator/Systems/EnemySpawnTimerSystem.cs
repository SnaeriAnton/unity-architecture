using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class EnemySpawnTimerSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;
        private readonly Func<bool> _canSpawn;
        private readonly Func<float> _getInterval;

        public EnemySpawnTimerSystem(EcsWorld world, Func<bool> canSpawn, Func<float> getInterval, CommandBuffer commandBuffer)
        {
            _world = world;
            _canSpawn = canSpawn;
            _getInterval = getInterval;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            if (!_world.Filter().With<PlayingTag>().Any()) return;
            if (!_canSpawn.Invoke()) return;

            List<Entity> timerEntities = _world.Filter().With<EnemySpawnTimer>().ToList();
            int spawnCount = 0;

            for (int i = 0; i < timerEntities.Count; i++)
            {
                Entity entity = timerEntities[i];

                EnemySpawnTimer timer = _world.Get<EnemySpawnTimer>(entity);

                timer.TimeLeft += deltaTime;

                if (timer.TimeLeft >= _getInterval.Invoke())
                {
                    spawnCount++;
                    timer.TimeLeft = 0;
                }

                _world.Set(entity, timer);
            }

            for (int i = 0; i < spawnCount; i++)
            {
                Entity request = _commandBuffer.CreateEntity();
                _commandBuffer.Add(request, new EnemySpawnRequest());
            }
        }
    }
}