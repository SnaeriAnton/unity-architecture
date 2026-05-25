namespace Game
{
    public class PlayerWeaponUpgradeSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;
        private readonly PlayerWeaponRegistry _playerWeaponRegistry;

        public PlayerWeaponUpgradeSystem(
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
            foreach (Entity request in _world.Filter().With<WeaponAddRequest>())
            {
                WeaponAddRequest data = _world.Get<WeaponAddRequest>(request);

                _playerWeaponRegistry.AddWeapon(data.Name, data.Weapon);
                _commandBuffer.DestroyEntity(request);
            }

            foreach (Entity request in _world.Filter().With<WeaponStatsSetRequest>())
            {
                WeaponStatsSetRequest data = _world.Get<WeaponStatsSetRequest>(request);

                _playerWeaponRegistry.SetStats(data.Name, data.Stats);
                
                if (data.Name == Weapons.Shield)
                {
                    Entity re = _commandBuffer.CreateEntity();

                    _commandBuffer.Add(re, new PlayerShieldSetRequest
                    {
                        IsActive = true,
                        Cooldown = data.Stats.CoolDown
                    });
                }
                
                _commandBuffer.DestroyEntity(request);
            }
        }
    }
}