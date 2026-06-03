using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class EnemyMeleeAttackSystem : IEcsRunSystem
    {
        private readonly PlayerApi _playerApi;

        public EnemyMeleeAttackSystem(PlayerApi playerApi)
        {
            _playerApi = playerApi;
        }

        public void Run(IEcsSystems systems)
        {
            if (!_playerApi.HasPlayingTag())
                return;

            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<EnemyTag>().Inc<EnemyMeleeAttack>().Inc<EnemyPlayerInRangeTag>().Exc<EnemyDeadTag>().End();

            EcsPool<EnemyMeleeAttack> attackPool = world.GetPool<EnemyMeleeAttack>();

            float deltaTime = Time.deltaTime;

            foreach (int enemy in filter)
            {
                ref EnemyMeleeAttack attack = ref attackPool.Get(enemy);

                attack.Timer -= deltaTime;

                if (attack.Timer > 0f)
                    continue;

                _playerApi.RequestPlayerDamage(attack.Damage);

                attack.Timer = attack.Cooldown;
            }
        }
    }
}