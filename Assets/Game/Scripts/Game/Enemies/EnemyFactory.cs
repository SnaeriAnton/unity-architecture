using UnityEngine;

namespace Game
{
    public class EnemyFactory
    {
        private readonly CommandBuffer _commandBuffer;

        public EnemyFactory(CommandBuffer commandBuffer) => _commandBuffer = commandBuffer;

        public Entity CreateEnemy(EnemyBase enemy, Transform enemyTransform, float speed, float health, int damage, float attackCooldown)
        {
            Entity enemyEntity = _commandBuffer.CreateEntity();

            _commandBuffer.Add(enemyEntity, new EnemyTag());
            _commandBuffer.Add(enemyEntity, new Position { Value = enemyTransform.position });
            _commandBuffer.Add(enemyEntity, new MoveSpeed { Value = speed });
            _commandBuffer.Add(enemyEntity, new EnemyViewRef { Transform = enemyTransform });
            _commandBuffer.Add(enemyEntity, new EnemyMonoRef { Enemy = enemy });
            _commandBuffer.Add(enemyEntity, new GameRuntimeTag());
            _commandBuffer.Add(enemyEntity, new EnemyHealth
            {
                Current = health,
                Max = health
            });
            _commandBuffer.Add(enemyEntity, new EnemyMeleeAttack
            {
                Damage = damage,
                Cooldown = attackCooldown,
                Timer = 0f
            });

            return enemyEntity;
        }
    }
}