using Leopotam.EcsLite;

namespace Game
{
    public class EnemySpawnerApi
    {
        private readonly EcsWorld _world;
        private int _spawnerEntity = -1;

        public EnemySpawnerApi(EcsWorld world) => _world = world;

        public void RegisterSpawner()
        {
            if (_spawnerEntity >= 0)
                return;

            _spawnerEntity = _world.NewEntity();

            ref EnemySpawnTimer timer = ref _world
                .GetPool<EnemySpawnTimer>()
                .Add(_spawnerEntity);

            timer.TimeLeft = 0f;
        }

        public void ResetTimer()
        {
            if (_spawnerEntity < 0)
                return;

            EcsPool<EnemySpawnTimer> timerPool = _world.GetPool<EnemySpawnTimer>();

            if (!timerPool.Has(_spawnerEntity))
                return;

            ref EnemySpawnTimer timer = ref timerPool.Get(_spawnerEntity);
            timer.TimeLeft = 0f;
        }

        public void RequestNextStage()
        {
            int request = _world.NewEntity();
            _world.GetPool<EnemySpawnStageNextRequest>().Add(request);
        }
    }
}