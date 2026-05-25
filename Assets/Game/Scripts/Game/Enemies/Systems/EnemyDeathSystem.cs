namespace Game
{
    public class EnemyDeathSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public EnemyDeathSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity enemy in _world.Filter().With<EnemyTag>().With<EnemyHealth>().With<EnemyMonoRef>().With<EnemyViewRef>())
            {
                if (_world.Has<EnemyDeathHandledTag>(enemy)) continue;

                EnemyHealth health = _world.Get<EnemyHealth>(enemy);

                if (health.Current > 0) continue;

                if (!_world.Has<EnemyDeadTag>(enemy))
                    _commandBuffer.AddIfMissing(enemy, new EnemyDeadTag());

                _commandBuffer.AddIfMissing(enemy, new EnemyDeathHandledTag());

                EnemyMonoRef monoRef = _world.Get<EnemyMonoRef>(enemy);
                EnemyViewRef viewRef = _world.Get<EnemyViewRef>(enemy);

                Entity killedRequest = _commandBuffer.CreateEntity();
                _commandBuffer.Add(killedRequest, new EnemyKilledRequest());

                Entity dropRequest = _commandBuffer.CreateEntity();
                _commandBuffer.Add(dropRequest, new EnemyDropRequest
                {
                    Position = viewRef.Transform.position
                });

                Entity despawnRequest = _commandBuffer.CreateEntity();
                _commandBuffer.Add(despawnRequest, new EnemyDespawnRequest
                {
                    Enemy = monoRef.Enemy
                });
            }
        }
    }
}