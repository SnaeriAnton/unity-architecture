using System.Linq;

namespace Game
{
    public class OrbitWeaponRotationSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public OrbitWeaponRotationSystem(EcsWorld world) => _world = world;

        public void Run(float deltaTime)
        {
            if (!_world.Filter().With<PlayingTag>().Any()) return;

            foreach (Entity weapon in _world.Filter().With<OrbitWeaponTag>().With<OrbitWeaponRotation>())
            {
                OrbitWeaponRotation rotation = _world.Get<OrbitWeaponRotation>(weapon);

                rotation.Angle += rotation.DegreesPerSecond * deltaTime;

                _world.Set(weapon, rotation);
            }
        }
    }
}