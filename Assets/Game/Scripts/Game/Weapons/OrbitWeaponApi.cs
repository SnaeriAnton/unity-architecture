using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class OrbitWeaponApi
    {
        private readonly EcsWorld _world;

        public OrbitWeaponApi(EcsWorld world) => _world = world;

        public int RegisterOrbitWeapon(Transform transform, float degreesPerSecond)
        {
            int entity = _world.NewEntity();

            _world.GetPool<OrbitWeaponTag>().Add(entity);

            ref OrbitWeaponViewRef viewRef = ref _world
                .GetPool<OrbitWeaponViewRef>()
                .Add(entity);

            viewRef.Transform = transform;

            ref OrbitWeaponRotation rotation = ref _world
                .GetPool<OrbitWeaponRotation>()
                .Add(entity);

            rotation.Angle = transform.localEulerAngles.z;
            rotation.DegreesPerSecond = degreesPerSecond;

            return entity;
        }

        public void RequestSetOrbitWeaponRotation(int weapon, float degreesPerSecond)
        {
            int request = _world.NewEntity();

            ref OrbitWeaponRotationSetRequest data = ref _world
                .GetPool<OrbitWeaponRotationSetRequest>()
                .Add(request);

            data.Weapon = weapon;
            data.DegreesPerSecond = degreesPerSecond;
        }
    }
}