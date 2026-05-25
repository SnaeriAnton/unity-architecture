namespace Game
{
    public class ProjectileViewSyncSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public ProjectileViewSyncSystem(EcsWorld world) => _world = world;

        public void Run(float deltaTime)
        {
            foreach (Entity projectile in _world.Filter().With<ProjectileTag>().With<Position>().With<Rotation>().With<ProjectileViewRef>())
            {
                Position position = _world.Get<Position>(projectile);
                Rotation rotation = _world.Get<Rotation>(projectile);
                ProjectileViewRef viewRef = _world.Get<ProjectileViewRef>(projectile);

                viewRef.Transform.SetPositionAndRotation(position.Value, rotation.Value);
            }
        }
    }
}