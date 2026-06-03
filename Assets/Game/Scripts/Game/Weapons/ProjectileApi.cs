using System;
using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class ProjectileApi
    {
        private readonly EcsWorld _world;

        public ProjectileApi(EcsWorld world) => _world = world;

        public int RegisterProjectile(
            Transform transform,
            ProjectileEcsLink link,
            Action despawn,
            Vector2 position,
            Vector2 direction,
            Quaternion rotation,
            float speed,
            float lifetime,
            float damage)
        {
            int entity = _world.NewEntity();

            _world.GetPool<ProjectileTag>().Add(entity);

            ref Position projectilePosition = ref _world.GetPool<Position>().Add(entity);
            projectilePosition.Value = position;

            ref Direction projectileDirection = ref _world.GetPool<Direction>().Add(entity);
            projectileDirection.Value = direction.normalized;

            ref Rotation projectileRotation = ref _world.GetPool<Rotation>().Add(entity);
            projectileRotation.Value = rotation;

            ref MoveSpeed projectileSpeed = ref _world.GetPool<MoveSpeed>().Add(entity);
            projectileSpeed.Value = speed;

            ref Lifetime projectileLifetime = ref _world.GetPool<Lifetime>().Add(entity);
            projectileLifetime.TimeLeft = lifetime;

            ref Damage projectileDamage = ref _world.GetPool<Damage>().Add(entity);
            projectileDamage.Value = damage;

            ref ProjectileViewRef viewRef = ref _world.GetPool<ProjectileViewRef>().Add(entity);
            viewRef.Transform = transform;
            viewRef.Link = link;
            viewRef.Despawn = despawn;

            return entity;
        }
        
        public void RequestProjectileHitEnemy(int projectileEntity, int enemyEntity)
        {
            EcsPool<ProjectileHitEnemyTarget> hitTargetPool = 
                _world.GetPool<ProjectileHitEnemyTarget>();

            if (hitTargetPool.Has(projectileEntity))
                return;

            ref ProjectileHitEnemyTarget hitTarget = ref hitTargetPool.Add(projectileEntity);
            hitTarget.Enemy = enemyEntity;
        }
    }
}