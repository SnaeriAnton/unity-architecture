using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class OrbitWeaponViewSyncSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<OrbitWeaponTag>().Inc<OrbitWeaponRotation>().Inc<OrbitWeaponViewRef>().End();

            EcsPool<OrbitWeaponRotation> rotationPool = world.GetPool<OrbitWeaponRotation>();
            EcsPool<OrbitWeaponViewRef> viewPool = world.GetPool<OrbitWeaponViewRef>();

            foreach (int weapon in filter)
            {
                ref OrbitWeaponRotation rotation = ref rotationPool.Get(weapon);
                ref OrbitWeaponViewRef viewRef = ref viewPool.Get(weapon);

                viewRef.Transform.localRotation = Quaternion.Euler(0f, 0f, rotation.Angle);
            }
        }
    }
}