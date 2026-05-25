namespace Game
{
    public class AxeLifetimeSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public AxeLifetimeSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity axeEntity in _world.Filter().With<AxeTag>().With<Lifetime>().With<AxeViewRef>().Without<AxeDespawnRequestedTag>())
            {
                Lifetime lifetime = _world.Get<Lifetime>(axeEntity);

                lifetime.TimeLeft -= deltaTime;

                if (lifetime.TimeLeft <= 0f)
                {
                    AxeViewRef viewRef = _world.Get<AxeViewRef>(axeEntity);
                    Entity request = _commandBuffer.CreateEntity();

                    _commandBuffer.Add(request, new AxeDespawnRequest
                    {
                        Axe = viewRef.Axe
                    });
                    
                    _commandBuffer.AddIfMissing(axeEntity, new AxeDespawnRequestedTag());

                    continue;
                }

                _world.Set(axeEntity, lifetime);
            }
        }
    }
}