namespace Game
{
    public class PlayerShieldSetSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public PlayerShieldSetSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity requestEntity in _world.Filter().With<PlayerShieldSetRequest>())
            {
                PlayerShieldSetRequest request = _world.Get<PlayerShieldSetRequest>(requestEntity);

                foreach (Entity player in _world.Filter().With<PlayerTag>().With<PlayerShield>())
                {
                    PlayerShield shield = _world.Get<PlayerShield>(player);

                    shield.IsActive = request.IsActive;
                    shield.Cooldown = request.Cooldown;

                    if (shield.Current > shield.Cooldown)
                        shield.Current = shield.Cooldown;

                    _world.Set(player, shield);

                    Entity hudRequest = _commandBuffer.CreateEntity();
                    _commandBuffer.Add(hudRequest, new HudRefreshRequest());
                }

                _commandBuffer.DestroyEntity(requestEntity);
            }
        }
    }
}