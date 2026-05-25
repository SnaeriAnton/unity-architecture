namespace Game
{
    public class AxeMovementSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public AxeMovementSystem(EcsWorld world) => _world = world;

        public void Run(float deltaTime)
        {
            foreach (Entity axe in _world.Filter().With<AxeTag>().With<Position>().With<Direction>().With<Rotation>().With<MoveSpeed>().With<RotationSpeed>())
            {
                Position position = _world.Get<Position>(axe);
                Direction direction = _world.Get<Direction>(axe);
                MoveSpeed speed = _world.Get<MoveSpeed>(axe);
                Rotation rotate = _world.Get<Rotation>(axe);
                RotationSpeed rotationSpeed = _world.Get<RotationSpeed>(axe);

                position.Value += direction.Value * speed.Value * deltaTime;
                rotate.Value.z = 90f * rotationSpeed.Value * deltaTime;


                _world.Set(axe, position);
                _world.Set(axe, rotate);
            }
        }
    }
}