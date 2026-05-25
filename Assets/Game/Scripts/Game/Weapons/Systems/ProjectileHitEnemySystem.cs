namespace Game
{
    public class ProjectileHitEnemySystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public ProjectileHitEnemySystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity projectile in _world.Filter().With<ProjectileTag>().With<Damage>().With<ProjectileViewRef>().With<ProjectileHitEnemyTag>().With<ProjectileHitEnemyTarget>().Without<ProjectileDespawnRequestedTag>())
            {
                Damage damage = _world.Get<Damage>(projectile);
                ProjectileHitEnemyTarget target = _world.Get<ProjectileHitEnemyTarget>(projectile);

                Entity damageRequest = _commandBuffer.CreateEntity();
                _commandBuffer.Add(damageRequest, new EnemyDamageRequest
                {
                    Target = target.Enemy,
                    Amount = damage.Value
                });

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