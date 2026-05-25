using System;

namespace Game
{
    public class PlayerDeathSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;
        private readonly Action _onDied;

        public PlayerDeathSystem(EcsWorld world, Action onDied, CommandBuffer commandBuffer)
        {
            _world = world;
            _onDied = onDied;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity entity in _world.Filter().With<PlayerTag>().With<DeadTag>())
            {
                if (_world.Has<DeathHandledTag>(entity))
                    continue;

                _commandBuffer.AddIfMissing(entity, new DeathHandledTag());

                _onDied?.Invoke();
            }
        }
    }
}