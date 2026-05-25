namespace Game
{
    public class AxeHitPlayerSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public AxeHitPlayerSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            Entity playerEntity = default;
            bool hasPlayer = false;

            foreach (Entity player in _world.Filter().With<PlayerTag>())
            {
                playerEntity = player;
                hasPlayer = true;
                break;
            }

            if (!hasPlayer) return;


            foreach (Entity axeEntity in _world.Filter().With<AxeTag>().With<Damage>().With<AxeViewRef>().With<AxeHitPlayerTag>().Without<AxeDespawnRequestedTag>())
            {
                Damage damageInt = _world.Get<Damage>(axeEntity);
                AxeViewRef viewRef = _world.Get<AxeViewRef>(axeEntity);

                Entity damageRequest = _commandBuffer.CreateEntity();

                _commandBuffer.Add(damageRequest, new DamageRequest
                {
                    Target = playerEntity,
                    Amount = damageInt.Value
                });

                Entity despawnRequest = _commandBuffer.CreateEntity();

                _commandBuffer.Add(despawnRequest, new AxeDespawnRequest
                {
                    Axe = viewRef.Axe
                });
                
                _commandBuffer.AddIfMissing(axeEntity, new AxeDespawnRequestedTag());
            }
        }
    }
}