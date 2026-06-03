using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class EnemyAxeAttackSystem : IEcsRunSystem
    {
        private readonly PlayerApi _playerApi;

        public EnemyAxeAttackSystem(PlayerApi playerApi) => _playerApi = playerApi;

        public void Run(IEcsSystems systems)
        {
            if (!_playerApi.HasPlayingTag()) return;

            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<EnemyTag>().Inc<EnemyAxeAttack>().Inc<EnemyAxeSpawnRef>().Exc<EnemyDeadTag>().End();

            EcsPool<EnemyAxeAttack> attackPool = world.GetPool<EnemyAxeAttack>();
            EcsPool<EnemyAxeSpawnRef> spawnPool = world.GetPool<EnemyAxeSpawnRef>();

            float deltaTime = Time.deltaTime;

            foreach (int enemy in filter)
            {
                ref EnemyAxeAttack attack = ref attackPool.Get(enemy);

                attack.Timer -= deltaTime;

                if (attack.Timer > 0f)
                    continue;

                ref EnemyAxeSpawnRef spawnRef = ref spawnPool.Get(enemy);
                spawnRef.Spawn?.Invoke();

                attack.Timer = attack.Cooldown;
            }
        }
    }
}