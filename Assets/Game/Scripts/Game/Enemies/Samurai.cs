using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Samurai : Enemy<SamuraiStats>
    {
        public override void Construct(Transform playerTransform, PoolManager poolManager, GameApi gameApi)
        {
            base.Construct(playerTransform, poolManager, gameApi);
            Health = _stats.Stats.Health;
            Speed = _stats.Stats.Speed;
            Damage = _stats.Stats.Damage;
            AttackCooldown = _stats.Stats.AttacksPerSecond;
        }

        public override void SetupEcs(Entity entity, GameApi gameApi) => gameApi.AddEnemySuicideAttackTag(entity);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<PlayerHitbox>(out _)) return;

            if (_link.IsRegistered)
                GameApi.RequestEnemyAttack(_link.Entity);
        }
    }
}