using System;
using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class LeoEnemyApi
    {
        private readonly EcsWorld _world;

        public LeoEnemyApi(EcsWorld world) => _world = world;

        public int RegisterEnemy(
            EnemyBase enemy,
            Transform enemyTransform,
            float speed,
            float health,
            int damage,
            float attackCooldown)
        {
            int entity = _world.NewEntity();

            _world.GetPool<EnemyTag>().Add(entity);

            ref Position position = ref _world.GetPool<Position>().Add(entity);
            position.Value = enemyTransform.position;
            
            ref MoveSpeed moveSpeed = ref _world.GetPool<MoveSpeed>().Add(entity);
            moveSpeed.Value = speed;

            ref EnemyViewRef viewRef = ref _world.GetPool<EnemyViewRef>().Add(entity);
            viewRef.Transform = enemyTransform;

            ref EnemyMonoRef monoRef = ref _world.GetPool<EnemyMonoRef>().Add(entity);
            monoRef.Enemy = enemy;

            ref EnemyHealth enemyHealth = ref _world.GetPool<EnemyHealth>().Add(entity);
            enemyHealth.Current = health;
            enemyHealth.Max = health;

            ref EnemyMeleeAttack meleeAttack = ref _world.GetPool<EnemyMeleeAttack>().Add(entity);
            meleeAttack.Damage = damage;
            meleeAttack.Cooldown = attackCooldown;
            meleeAttack.Timer = 0f;

            return entity;
        }

        public void RequestEnemyDamage(int enemyEntity, float damage)
        {
            int request = _world.NewEntity();

            ref EnemyDamageRequest requestData = ref _world
                .GetPool<EnemyDamageRequest>()
                .Add(request);

            requestData.Target = enemyEntity;
            requestData.Amount = damage;
        }
        
        public void SetEnemyPlayerInRange(int enemyEntity, bool inRange)
        {
            EcsPool<EnemyPlayerInRangeTag> pool = _world.GetPool<EnemyPlayerInRangeTag>();

            if (inRange)
            {
                if (!pool.Has(enemyEntity))
                    pool.Add(enemyEntity);
            }
            else
            {
                if (pool.Has(enemyEntity))
                    pool.Del(enemyEntity);
            }
        }

        public void RequestEnemyAttack(int enemyEntity)
        {
            EcsPool<EnemyAttackRequestTag> pool = _world.GetPool<EnemyAttackRequestTag>();

            if (!pool.Has(enemyEntity))
                pool.Add(enemyEntity);
        }
        
        public void AddEnemySuicideAttackTag(int enemyEntity)
        {
            EcsPool<EnemySuicideAttackTag> pool = _world.GetPool<EnemySuicideAttackTag>();

            if (!pool.Has(enemyEntity))
                pool.Add(enemyEntity);
        }
        
        public void AddEnemyAxeAttack(
            int enemyEntity,
            float cooldown,
            Action spawn)
        {
            EcsPool<EnemyAxeAttack> attackPool = _world.GetPool<EnemyAxeAttack>();
            EcsPool<EnemyAxeSpawnRef> spawnPool = _world.GetPool<EnemyAxeSpawnRef>();

            if (!attackPool.Has(enemyEntity))
            {
                ref EnemyAxeAttack attack = ref attackPool.Add(enemyEntity);
                attack.Cooldown = cooldown;
                attack.Timer = cooldown;
            }

            if (!spawnPool.Has(enemyEntity))
            {
                ref EnemyAxeSpawnRef spawnRef = ref spawnPool.Add(enemyEntity);
                spawnRef.Spawn = spawn;
            }
        }
    }
}