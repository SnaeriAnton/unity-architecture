namespace Game
{
    public class ProjectileLifetimeSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public ProjectileLifetimeSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity projectile in _world.Filter().With<ProjectileTag>().With<Lifetime>().With<ProjectileViewRef>().Without<ProjectileDespawnRequestedTag>())
            {
                Lifetime lifetime = _world.Get<Lifetime>(projectile);

                lifetime.TimeLeft -= deltaTime;

                if (lifetime.TimeLeft > 0f)
                {
                    _world.Set(projectile, lifetime);
                    continue;
                }

                Entity despawnRequest = _commandBuffer.CreateEntity();
                _commandBuffer.Add(despawnRequest, new ProjectileDespawnRequest
                {
                    Projectile = projectile
                });

                _commandBuffer.AddIfMissing(projectile, new ProjectileDespawnRequestedTag());
            }
        }
    }
}