using UnityEngine;

namespace Game
{
    public class OrbitWeaponViewSyncSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public OrbitWeaponViewSyncSystem(EcsWorld world) => _world = world;

        public void Run(float deltaTime)
        {
            foreach (Entity weapon in _world.Filter().With<OrbitWeaponTag>().With<OrbitWeaponRotation>().With<OrbitWeaponViewRef>())
            {
                OrbitWeaponRotation rotation = _world.Get<OrbitWeaponRotation>(weapon);
                OrbitWeaponViewRef viewRef = _world.Get<OrbitWeaponViewRef>(weapon);

                viewRef.Transform.localRotation = Quaternion.Euler(0f, 0f, rotation.Angle);
            }
        }
    }
}