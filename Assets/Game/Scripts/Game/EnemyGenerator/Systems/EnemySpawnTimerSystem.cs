using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class EnemySpawnTimerSystem : IEcsRunSystem
    {
        private readonly PlayerApi _playerApi;
        private readonly EnemySpawnerController _spawnerController;

        public EnemySpawnTimerSystem(PlayerApi playerApi, EnemySpawnerController spawnerController)
        {
            _playerApi = playerApi;
            _spawnerController = spawnerController;
        }

        public void Run(IEcsSystems systems)
        {
            if (!_playerApi.HasPlayingTag()) return;

            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<EnemySpawnTimer>().End();

            EcsPool<EnemySpawnTimer> timerPool = world.GetPool<EnemySpawnTimer>();
            EcsPool<EnemySpawnRequest> spawnRequestPool = world.GetPool<EnemySpawnRequest>();

            float deltaTime = Time.deltaTime;

            foreach (int entity in filter)
            {
                ref EnemySpawnTimer timer = ref timerPool.Get(entity);

                timer.TimeLeft -= deltaTime;

                if (timer.TimeLeft > 0f)
                    continue;

                int request = world.NewEntity();
                spawnRequestPool.Add(request);

                timer.TimeLeft = _spawnerController.CurrentSpawnInterval;
            }
        }
    }
}