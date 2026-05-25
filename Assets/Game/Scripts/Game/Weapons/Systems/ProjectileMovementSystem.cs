namespace Game
{
    public class ProjectileMovementSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public ProjectileMovementSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity projectile in _world.Filter().With<ProjectileTag>().With<Position>().With<Direction>().With<MoveSpeed>().Without<ProjectileDespawnRequestedTag>())
            {
                Position position = _world.Get<Position>(projectile);
                Direction direction = _world.Get<Direction>(projectile);
                MoveSpeed speed = _world.Get<MoveSpeed>(projectile);

                position.Value += direction.Value * speed.Value * deltaTime;

                _world.Set(projectile, position);
            }
        }
    }
}