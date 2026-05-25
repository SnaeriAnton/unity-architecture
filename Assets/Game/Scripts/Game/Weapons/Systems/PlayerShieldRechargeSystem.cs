namespace Game
{
    public class PlayerShieldRechargeSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public PlayerShieldRechargeSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity killedRequest in _world.Filter().With<EnemyKilledRequest>())
            {
                foreach (Entity player in _world.Filter().With<PlayerTag>().With<PlayerShield>())
                {
                    PlayerShield shield = _world.Get<PlayerShield>(player);

                    if (!shield.IsActive) continue;

                    if (shield.Current < shield.Cooldown)
                    {
                        shield.Current++;
                        _world.Set(player, shield);

                        Entity hudRequest = _commandBuffer.CreateEntity();
                        _commandBuffer.Add(hudRequest, new HudRefreshRequest());
                    }
                }

                _commandBuffer.DestroyEntity(killedRequest);
            }
        }
    }
}