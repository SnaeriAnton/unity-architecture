using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Samurai : Enemy<SamuraiStats>
    {
        public override void Construct(Transform playerTransform, PoolManager poolManager, AxeApi axeApi, LeoEnemyApi leoEnemyApi)
        {
            base.Construct(playerTransform, poolManager, axeApi, leoEnemyApi);
            Health = _stats.Stats.Health;
            Speed = _stats.Stats.Speed;
            Damage = _stats.Stats.Damage;
            AttackCooldown = _stats.Stats.AttacksPerSecond;
        }

        public override void SetupEcs(int entity) => _leoEnemyApi.AddEnemySuicideAttackTag(entity);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<PlayerHitbox>(out _)) return;

            if (_link.IsRegistered)
                _leoEnemyApi.RequestEnemyAttack(_link.Entity);
        }
    }
}