using Leopotam.EcsLite;

namespace Game
{
    public class EnemySuicideAttackSystem : IEcsRunSystem
    {
        private readonly PlayerApi _playerApi;

        public EnemySuicideAttackSystem(PlayerApi playerApi) => _playerApi = playerApi;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<EnemyTag>().Inc<EnemySuicideAttackTag>().Inc<EnemyAttackRequestTag>().Inc<EnemyMeleeAttack>().Exc<EnemyDeadTag>().End();

            EcsPool<EnemyMeleeAttack> attackPool = world.GetPool<EnemyMeleeAttack>();
            EcsPool<EnemyDeadTag> deadPool = world.GetPool<EnemyDeadTag>();
            EcsPool<EnemyAttackRequestTag> attackRequestPool = world.GetPool<EnemyAttackRequestTag>();

            foreach (int enemy in filter)
            {
                ref EnemyMeleeAttack attack = ref attackPool.Get(enemy);

                _playerApi.RequestPlayerDamage(attack.Damage);

                if (!deadPool.Has(enemy))
                    deadPool.Add(enemy);

                if (attackRequestPool.Has(enemy))
                    attackRequestPool.Del(enemy);
            }
        }
    }
}