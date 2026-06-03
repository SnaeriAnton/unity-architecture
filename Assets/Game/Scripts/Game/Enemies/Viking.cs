using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Viking : Enemy<VikingStats>
    {
        public override void Construct(Transform playerTransform, PoolManager poolManager, AxeApi axeApi, LeoEnemyApi leoEnemyApi)
        {
            base.Construct(playerTransform, poolManager, axeApi, leoEnemyApi);
            Health = _stats.Stats.Health;
            Speed = _stats.Stats.Speed;
            Damage = _stats.Stats.Damage;
            AttackCooldown = _stats.Stats.AttacksPerSecond;
        }

        public override void SetupEcs(int entity) => _leoEnemyApi.AddEnemyAxeAttack(entity, _stats.AxeStats.AttacksPerSecond, ShootAxe);

        private void ShootAxe()
        {
            Vector2 direction = _playerTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Axe axe = _poolManager.Spawn(_stats.AxeTemplate, transform.position, Quaternion.Euler(0f, 0f, angle));
            axe.Init(AxeApi, _stats.AxeStats, direction.normalized);
        }
    }
}