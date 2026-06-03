using System;
using Leopotam.EcsLite;

namespace Game
{
    public class ProjectileWeaponApi
    {
        private readonly EcsWorld _world;

        public ProjectileWeaponApi(EcsWorld world) => _world = world;

        public int RegisterProjectileWeapon(float cooldown, Action spawn)
        {
            int entity = _world.NewEntity();

            _world.GetPool<ProjectileWeaponTag>().Add(entity);

            ref ProjectileWeaponAttack attack = ref _world.GetPool<ProjectileWeaponAttack>().Add(entity);

            attack.Cooldown = cooldown;
            attack.Timer = cooldown;

            ref ProjectileWeaponSpawnRef spawnRef = ref _world.GetPool<ProjectileWeaponSpawnRef>().Add(entity);

            spawnRef.Spawn = spawn;

            return entity;
        }

        public void RequestSetProjectileWeaponCooldown(int weapon, float cooldown)
        {
            EcsPool<ProjectileWeaponCooldownSetRequest> pool = _world.GetPool<ProjectileWeaponCooldownSetRequest>();

            int request = _world.NewEntity();

            ref ProjectileWeaponCooldownSetRequest data = ref pool.Add(request);
            data.Weapon = weapon;
            data.Cooldown = cooldown;
        }
    }
}