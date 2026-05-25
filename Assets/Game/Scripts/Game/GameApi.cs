using System;
using UnityEngine;

namespace Game
{
    public class GameApi
    {
        private readonly EcsWorld _world;
        private readonly EnemyFactory _enemyFactory;
        private readonly AxeFactory _axeFactory;
        private readonly CommandBuffer _commandBuffer;
        private readonly ProjectileFactory _projectileFactory;
        private readonly WeaponHitboxFactory _weaponHitboxFactory;
        private readonly ProjectileWeaponFactory _projectileWeaponFactory;
        private readonly OrbitWeaponFactory _orbitWeaponFactory;

        public GameApi(EcsWorld world, EnemyFactory enemyFactory, AxeFactory axeFactory, CommandBuffer commandBuffer, ProjectileFactory projectileFactory, WeaponHitboxFactory weaponHitboxFactory, ProjectileWeaponFactory projectileWeaponFactory, OrbitWeaponFactory orbitWeaponFactory)
        {
            _world = world;
            _enemyFactory = enemyFactory;
            _axeFactory = axeFactory;
            _commandBuffer = commandBuffer;
            _projectileFactory = projectileFactory;
            _weaponHitboxFactory = weaponHitboxFactory;
            _projectileWeaponFactory = projectileWeaponFactory;
            _orbitWeaponFactory = orbitWeaponFactory;
        }
        
        public Entity RegisterWeaponHitbox(Transform transform, float damage) => _weaponHitboxFactory.Create(transform, damage);
        public Entity RegisterProjectileWeapon(float cooldown, Action spawn) => _projectileWeaponFactory.Create(cooldown, spawn);
        public Entity RegisterOrbitWeapon(Transform transform, float degreesPerSecond) => _orbitWeaponFactory.Create(transform, degreesPerSecond);

        public Entity RegisterEnemy(
            EnemyBase enemy,
            Transform transform,
            float speed,
            float health,
            int damage,
            float attackCooldown)
        {
            return _enemyFactory.CreateEnemy(
                enemy,
                transform,
                speed,
                health,
                damage,
                attackCooldown);
        }
        
        public void RequestEnemyDamage(Entity enemyEntity, float damage)
        {
            Entity request = _commandBuffer.CreateEntity();

            _commandBuffer.Add(request, new EnemyDamageRequest
            {
                Target = enemyEntity,
                Amount = damage
            });
        }

        public void RequestEnemyAttack(Entity enemyEntity)
        {
            if (!_world.Has<EnemyTag>(enemyEntity)) return;

            if (!_world.Has<EnemyAttackRequestTag>(enemyEntity))
                _commandBuffer.AddIfMissing(enemyEntity, new EnemyAttackRequestTag());
        }

        public void SetEnemyPlayerInRange(Entity enemyEntity, bool inRange)
        {
            if (!_world.Has<EnemyTag>(enemyEntity)) return;

            if (inRange)
            {
                if (!_world.Has<EnemyPlayerInRangeTag>(enemyEntity))
                    _commandBuffer.AddIfMissing(enemyEntity, new EnemyPlayerInRangeTag());
            }
            else
            {
                if (_world.Has<EnemyPlayerInRangeTag>(enemyEntity))
                    _commandBuffer.Remove<EnemyPlayerInRangeTag>(enemyEntity);
            }
        }

        public void AddEnemySuicideAttackTag(Entity enemyEntity)
        {
            if (!_world.Has<EnemySuicideAttackTag>(enemyEntity))
                _commandBuffer.Add(enemyEntity, new EnemySuicideAttackTag());
        }

        public void AddEnemyAxeAttack(Entity enemyEntity, float cooldown, Action spawnAxe)
        {
            if (!_world.Has<EnemyAxeAttack>(enemyEntity))
            {
                _commandBuffer.Add(enemyEntity, new EnemyAxeAttack
                {
                    Cooldown = cooldown,
                    Timer = cooldown
                });
            }

            if (!_world.Has<EnemyAxeSpawnRef>(enemyEntity))
                _commandBuffer.Add(enemyEntity, new EnemyAxeSpawnRef { Spawn = spawnAxe });
        }

        public Entity RegisterAxe(
            Axe axe,
            Vector2 position,
            Vector2 direction,
            Quaternion rotation,
            float speed,
            float lifetime,
            int damage,
            float rotationSpeed)
        {
            return _axeFactory.CreateAxe(
                axe,
                position,
                direction,
                rotation,
                speed,
                lifetime,
                damage, 
                rotationSpeed);
        }
        
        public void RequestAxeHitPlayer(Entity axeEntity)
        {
            if (!_world.Has<AxeTag>(axeEntity)) return;
            if (!_world.Has<AxeHitPlayerTag>(axeEntity))
                _commandBuffer.AddIfMissing(axeEntity, new AxeHitPlayerTag());
        }
        
        public Entity RegisterProjectile(
            Transform transform,
            ProjectileEcsLink link,
            Action despawn,
            Vector2 position,
            Vector2 direction,
            Quaternion rotation,
            float speed,
            float lifetime,
            float damage)
        {
            return _projectileFactory.CreateProjectile(
                transform,
                link,
                despawn,
                position,
                direction,
                rotation,
                speed,
                lifetime,
                damage);
        }

        public void RequestProjectileHitEnemy(Entity projectileEntity, Entity enemyEntity)
        {
            if (!_world.Has<ProjectileTag>(projectileEntity)) return;

            if (!_world.Has<ProjectileHitEnemyTag>(projectileEntity))
                _commandBuffer.AddIfMissing(projectileEntity, new ProjectileHitEnemyTag());

            if (!_world.Has<ProjectileHitEnemyTarget>(projectileEntity))
                _commandBuffer.AddIfMissing(projectileEntity, new ProjectileHitEnemyTarget { Enemy = enemyEntity });
        }

        public void RequestWeaponHitboxHitEnemy(Entity weaponHitbox, Entity enemy)
        {
            Entity request = _commandBuffer.CreateEntity();

            _commandBuffer.Add(request, new WeaponHitboxHitEnemyRequest
            {
                WeaponHitbox = weaponHitbox,
                Enemy = enemy
            });
        }
        
        public void RequestSetWeaponHitboxDamage(Entity weaponHitbox, float damage)
        {
            if (!_world.Has<WeaponHitboxTag>(weaponHitbox)) return;

            Entity request = _commandBuffer.CreateEntity();

            _commandBuffer.Add(request, new WeaponHitboxDamageSetRequest
            {
                WeaponHitbox = weaponHitbox,
                Damage = damage
            });
        }

        public void RequestSetProjectileWeaponCooldown(Entity weapon, float cooldown)
        {
            Entity request = _commandBuffer.CreateEntity();

            _commandBuffer.Add(request, new ProjectileWeaponCooldownSetRequest
            {
                Weapon = weapon,
                Cooldown = cooldown
            });
        }
        

        public void RequestSetOrbitWeaponRotation(Entity weapon, float degreesPerSecond)
        {
            Entity request = _commandBuffer.CreateEntity();

            _commandBuffer.Add(request, new OrbitWeaponRotationSetRequest
            {
                Weapon = weapon,
                DegreesPerSecond = degreesPerSecond
            });
        }
    }
}