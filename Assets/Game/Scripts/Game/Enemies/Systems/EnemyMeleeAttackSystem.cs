using System.Linq;

namespace Game
{
    public class EnemyMeleeAttackSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public EnemyMeleeAttackSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            if (!_world.Filter().With<PlayingTag>().Any()) return;

            Entity playerEntity = default;
            bool hasPlayer = false;

            foreach (Entity player in _world.Filter().With<PlayerTag>())
            {
                playerEntity = player;
                hasPlayer = true;
                break;
            }

            if (!hasPlayer)
                return;
            
            foreach (Entity enemy in _world.Filter().With<EnemyTag>().With<EnemyMeleeAttack>().With<EnemyPlayerInRangeTag>())
            {
                if (_world.Has<EnemyDeadTag>(enemy))
                    continue;

                EnemyMeleeAttack attack = _world.Get<EnemyMeleeAttack>(enemy);

                attack.Timer -= deltaTime;

                if (attack.Timer <= 0f)
                {
                    Entity damageRequest = _commandBuffer.CreateEntity();

                    _commandBuffer.Add(damageRequest, new DamageRequest
                    {
                        Target = playerEntity,
                        Amount = attack.Damage
                    });
                    
                    attack.Timer = attack.Cooldown;
                }

                _world.Set(enemy, attack);
            }
        }
    }
}