using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Knight : Enemy<KnightStats>
    {
        public override void Construct(Transform playerTransform, PoolManager poolManager, GameApi gameApi)
        {
            base.Construct(playerTransform, poolManager, gameApi);
            Health = _stats.Stats.Health;
            Speed = _stats.Stats.Speed;
            Damage = _stats.Stats.Damage;
            AttackCooldown = _stats.Stats.AttacksPerSecond;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<PlayerHitbox>(out _)) return;
            
            if (_link.IsRegistered)
                GameApi.SetEnemyPlayerInRange(_link.Entity, true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent<PlayerHitbox>(out _)) return;
            
            if (_link.IsRegistered)
                GameApi.SetEnemyPlayerInRange(_link.Entity, false);
        }
    }
}