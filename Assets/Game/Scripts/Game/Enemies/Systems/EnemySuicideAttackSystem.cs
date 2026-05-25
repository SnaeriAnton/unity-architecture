namespace Game
{
    public class EnemySuicideAttackSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public EnemySuicideAttackSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            Entity playerEntity = default;
            bool hasPlayer = false;

            foreach (Entity player in _world.Filter().With<PlayerTag>())
            {
                playerEntity = player;
                hasPlayer = true;
                break;
            }

            if (!hasPlayer) return;

            foreach (Entity enemy in _world.Filter().With<EnemyTag>().With<EnemyMeleeAttack>().With<EnemySuicideAttackTag>().With<EnemyAttackRequestTag>())
            {
                if (_world.Has<EnemyDeadTag>(enemy)) continue;

                EnemyMeleeAttack attack = _world.Get<EnemyMeleeAttack>(enemy);

                Entity playerDamageRequest = _commandBuffer.CreateEntity();

                _commandBuffer.Add(playerDamageRequest, new DamageRequest
                {
                    Target = playerEntity,
                    Amount = attack.Damage
                });

                EnemyHealth health = _world.Get<EnemyHealth>(enemy);
                Entity enemyDamageRequest = _commandBuffer.CreateEntity();

                _commandBuffer.Add(enemyDamageRequest, new EnemyDamageRequest
                {
                    Target = enemy,
                    Amount = health.Current
                });

                _commandBuffer.Remove<EnemyAttackRequestTag>(enemy);
            }
        }
    }
}