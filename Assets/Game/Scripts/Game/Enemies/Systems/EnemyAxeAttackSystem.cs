using System.Linq;

namespace Game
{
    public class EnemyAxeAttackSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public EnemyAxeAttackSystem(EcsWorld world) => _world = world;

        public void Run(float deltaTime)
        {
            if (!_world.Filter().With<PlayingTag>().Any()) return;

            foreach (Entity enemy in _world.Filter().With<EnemyTag>().With<EnemyAxeAttack>().With<EnemyAxeSpawnRef>())
            {
                if (_world.Has<EnemyDeadTag>(enemy))
                    continue;

                EnemyAxeAttack attack = _world.Get<EnemyAxeAttack>(enemy);

                attack.Timer -= deltaTime;

                if (attack.Timer <= 0f)
                {
                    EnemyAxeSpawnRef spawnRef = _world.Get<EnemyAxeSpawnRef>(enemy);
                    spawnRef.Spawn.Invoke();
                    attack.Timer = attack.Cooldown;
                }

                _world.Set(enemy, attack);
            }
        }
    }
}