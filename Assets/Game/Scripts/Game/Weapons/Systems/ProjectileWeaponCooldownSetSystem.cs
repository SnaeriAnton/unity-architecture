namespace Game
{
    public class ProjectileWeaponCooldownSetSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public ProjectileWeaponCooldownSetSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity requestEntity in _world.Filter().With<ProjectileWeaponCooldownSetRequest>())
            {
                ProjectileWeaponCooldownSetRequest request = _world.Get<ProjectileWeaponCooldownSetRequest>(requestEntity);

                if (_world.Has<ProjectileWeaponAttack>(request.Weapon))
                {
                    ProjectileWeaponAttack attack = _world.Get<ProjectileWeaponAttack>(request.Weapon);

                    attack.Cooldown = request.Cooldown;

                    if (attack.Timer > attack.Cooldown)
                        attack.Timer = attack.Cooldown;

                    _world.Set(request.Weapon, attack);
                }

                _commandBuffer.DestroyEntity(requestEntity);
            }
        }
    }
}