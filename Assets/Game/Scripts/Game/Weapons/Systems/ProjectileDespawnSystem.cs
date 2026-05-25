namespace Game
{
    public class ProjectileDespawnSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public ProjectileDespawnSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity requestEntity in _world.Filter().With<ProjectileDespawnRequest>())
            {
                ProjectileDespawnRequest request = _world.Get<ProjectileDespawnRequest>(requestEntity);

                if (_world.Has<ProjectileViewRef>(request.Projectile))
                {
                    ProjectileViewRef viewRef = _world.Get<ProjectileViewRef>(request.Projectile);

                    if (viewRef.Link.IsRegistered) viewRef.Link.Clear();

                    viewRef.Despawn.Invoke();
                }

                _commandBuffer.DestroyEntity(request.Projectile);
                _commandBuffer.DestroyEntity(requestEntity);
            }
        }
    }
}