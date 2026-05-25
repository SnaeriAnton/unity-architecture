using System;
using UnityEngine;

namespace Game
{
    public class ProjectileFactory
    {
        private readonly CommandBuffer _commandBuffer;

        public ProjectileFactory(CommandBuffer commandBuffer) => _commandBuffer = commandBuffer;

        public Entity CreateProjectile(
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
            Entity entity = _commandBuffer.CreateEntity();

            _commandBuffer.Add(entity, new ProjectileTag());
            _commandBuffer.Add(entity, new ProjectileViewRef
            {
                Transform = transform,
                Link = link,
                Despawn = despawn,
            });
            _commandBuffer.Add(entity, new PlayerWeaponRuntimeTag());
            _commandBuffer.Add(entity, new Position { Value = position });
            _commandBuffer.Add(entity, new Direction { Value = direction.normalized });
            _commandBuffer.Add(entity, new Rotation { Value = rotation });
            _commandBuffer.Add(entity, new MoveSpeed { Value = speed });
            _commandBuffer.Add(entity, new Lifetime { TimeLeft = lifetime });
            _commandBuffer.Add(entity, new Damage { Value = damage });
            _commandBuffer.Add(entity, new GameRuntimeTag());

            return entity;
        }
    }
}