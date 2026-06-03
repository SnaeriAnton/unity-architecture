using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class AxeApi
    {
        private readonly EcsWorld _world;

        public AxeApi(EcsWorld world) => _world = world;

        public int RegisterAxe(
            Axe axe,
            Transform transform,
            Action despawn,
            Vector2 position,
            Vector2 direction,
            Quaternion rotation,
            float speed,
            float lifetime,
            float damage,
            float rotationSpeed)
        {
            int entity = _world.NewEntity();

            _world.GetPool<AxeTag>().Add(entity);

            ref Position positionComponent = ref _world.GetPool<Position>().Add(entity);
            positionComponent.Value = position;

            ref Direction directionComponent = ref _world.GetPool<Direction>().Add(entity);
            directionComponent.Value = direction.normalized;

            ref Rotation rotationComponent = ref _world.GetPool<Rotation>().Add(entity);
            rotationComponent.Value = rotation;

            ref MoveSpeed speedComponent = ref _world.GetPool<MoveSpeed>().Add(entity);
            speedComponent.Value = speed;

            ref Lifetime lifetimeComponent = ref _world.GetPool<Lifetime>().Add(entity);
            lifetimeComponent.TimeLeft = lifetime;

            ref Damage damageComponent = ref _world.GetPool<Damage>().Add(entity);
            damageComponent.Value = damage;

            ref RotationSpeed rotationSpeedComponent = ref _world.GetPool<RotationSpeed>().Add(entity);
            rotationSpeedComponent.Value = rotationSpeed;

            ref AxeViewRef viewRef = ref _world.GetPool<AxeViewRef>().Add(entity);
            viewRef.Axe = axe;
            viewRef.Transform = transform;
            viewRef.Despawn = despawn;

            return entity;
        }

        public void RequestAxeHitPlayer(int axeEntity)
        {
            EcsPool<AxeHitPlayerTag> hitPool = _world.GetPool<AxeHitPlayerTag>();

            if (!hitPool.Has(axeEntity))
                hitPool.Add(axeEntity);
        }
    }
}