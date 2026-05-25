namespace Game
{
    public class PlayerWeaponResetSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;
        private readonly PlayerWeaponRegistry _playerWeaponRegistry;

        public PlayerWeaponResetSystem(
            EcsWorld world,
            CommandBuffer commandBuffer,
            PlayerWeaponRegistry playerWeaponRegistry)
        {
            _world = world;
            _commandBuffer = commandBuffer;
            _playerWeaponRegistry = playerWeaponRegistry;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity request in _world.Filter().With<WeaponResetRequest>())
            {
                _playerWeaponRegistry.Reset();
                
                foreach (Entity entity in _world.Filter().With<PlayerWeaponRuntimeTag>())
                    _commandBuffer.DestroyEntity(entity);
                
                Entity shieldResetRequest = _commandBuffer.CreateEntity();

                _commandBuffer.Add(shieldResetRequest, new PlayerShieldSetRequest
                {
                    IsActive = false,
                    Cooldown = 0
                });
                _commandBuffer.DestroyEntity(request);
            }
        }
    }
}