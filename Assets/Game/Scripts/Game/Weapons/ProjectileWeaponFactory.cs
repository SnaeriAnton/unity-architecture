using System;

namespace Game
{
    public class ProjectileWeaponFactory
    {
        private readonly CommandBuffer _commandBuffer;

        public ProjectileWeaponFactory(CommandBuffer commandBuffer) => _commandBuffer = commandBuffer;

        public Entity Create(float cooldown, Action spawn)
        {
            Entity entity = _commandBuffer.CreateEntity();

            _commandBuffer.Add(entity, new ProjectileWeaponTag());
            _commandBuffer.Add(entity, new PlayerWeaponRuntimeTag());
            _commandBuffer.Add(entity, new ProjectileWeaponSpawnRef { Spawn = spawn });
            _commandBuffer.Add(entity, new GameRuntimeTag());
            _commandBuffer.Add(entity, new ProjectileWeaponAttack
            {
                Cooldown = cooldown,
                Timer = cooldown
            });

            return entity;
        }
    }
}