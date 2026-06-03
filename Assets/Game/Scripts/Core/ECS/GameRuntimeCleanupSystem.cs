using Leopotam.EcsLite;

namespace Game
{
    public class GameRuntimeCleanupSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter requestFilter = world.Filter<LeoGameRuntimeCleanupRequest>().End();

            foreach (int request in requestFilter)
            {
                CleanupProjectiles(world);
                CleanupProjectileWeapons(world);
                CleanupProjectileWeaponRequests(world);

                CleanupWeaponHitboxes(world);
                CleanupWeaponHitboxRequests(world);

                CleanupOrbitWeapons(world);
                CleanupOrbitWeaponRequests(world);

                CleanupAxes(world);

                CleanupEnemies(world);

                DestroyAll<EnemyDamageRequest>(world);
                DestroyAll<EnemyDropRequest>(world);
                DestroyAll<EnemyDespawnRequest>(world);
                DestroyAll<EnemyKilledRequest>(world);
                DestroyAll<EnemySpawnRequest>(world);
                DestroyAll<EnemySpawnStageNextRequest>(world);

                DestroyAll<DamageRequest>(world);
                DestroyAll<PlayerShieldSetRequest>(world);
                DestroyAll<HudRefreshRequest>(world);
                
                DestroyAll<CoinPickupRequest>(world);
                DestroyAll<CrystalPickupRequest>(world);

                world.DelEntity(request);
            }
        }

        private void CleanupProjectiles(EcsWorld world)
        {
            EcsFilter projectileFilter = world.Filter<ProjectileTag>().Inc<ProjectileViewRef>().End();

            EcsPool<ProjectileViewRef> viewRefPool = world.GetPool<ProjectileViewRef>();

            foreach (int projectile in projectileFilter)
            {
                ref ProjectileViewRef viewRef = ref viewRefPool.Get(projectile);

                if (viewRef.Link != null)
                    viewRef.Link.Clear();

                viewRef.Despawn?.Invoke();

                world.DelEntity(projectile);
            }
        }

        private void CleanupEnemies(EcsWorld world)
        {
            EcsFilter filter = world.Filter<EnemyTag>().End();

            EcsPool<EnemyMonoRef> monoRefPool = world.GetPool<EnemyMonoRef>();

            foreach (int enemy in filter)
            {
                if (monoRefPool.Has(enemy))
                {
                    ref EnemyMonoRef monoRef = ref monoRefPool.Get(enemy);

                    if (monoRef.Enemy != null &&
                        monoRef.Enemy.TryGetComponent(out EnemyEcsLink link))
                    {
                        link.Clear();
                    }
                }

                world.DelEntity(enemy);
            }
        }
        
        private void CleanupProjectileWeapons(EcsWorld world)
        {
            EcsFilter weaponFilter = world.Filter<ProjectileWeaponTag>().End();

            foreach (int weapon in weaponFilter)
                world.DelEntity(weapon);
        }

        private void CleanupProjectileWeaponRequests(EcsWorld world)
        {
            EcsFilter requestFilter = world.Filter<ProjectileWeaponCooldownSetRequest>().End();

            foreach (int request in requestFilter)
                world.DelEntity(request);
        }
        
        private void CleanupWeaponHitboxes(EcsWorld world)
        {
            EcsFilter hitboxFilter = world.Filter<WeaponHitboxTag>().End();

            EcsPool<WeaponHitboxViewRef> viewRefPool = world.GetPool<WeaponHitboxViewRef>();

            foreach (int hitbox in hitboxFilter)
            {
                if (viewRefPool.Has(hitbox))
                {
                    ref WeaponHitboxViewRef viewRef = ref viewRefPool.Get(hitbox);

                    if (viewRef.Transform != null &&
                        viewRef.Transform.TryGetComponent(out WeaponHitboxEcsLink link))
                    {
                        link.Clear();
                    }
                }

                world.DelEntity(hitbox);
            }
        }
        
        private void CleanupWeaponHitboxRequests(EcsWorld world)
        {
            DestroyAll<WeaponHitboxHitEnemyRequest>(world);
            DestroyAll<WeaponHitboxDamageSetRequest>(world);
            DestroyAll<OrbitWeaponRotationSetRequest>(world);
        }

        private void DestroyAll<T>(EcsWorld world) where T : struct
        {
            EcsFilter filter = world.Filter<T>().End();

            foreach (int entity in filter)
                world.DelEntity(entity);
        }
        
        private void CleanupOrbitWeapons(EcsWorld world)
        {
            EcsFilter filter = world.Filter<OrbitWeaponTag>().End();

            foreach (int entity in filter)
                world.DelEntity(entity);
        }
        
        private void CleanupOrbitWeaponRequests(EcsWorld world)
        {
            EcsFilter filter = world.Filter<OrbitWeaponRotationSetRequest>().End();
        
            foreach (int entity in filter)
                world.DelEntity(entity);
        }
        
        private void CleanupAxes(EcsWorld world)
        {
            EcsFilter filter = world.Filter<AxeTag>().End();

            EcsPool<AxeViewRef> viewRefPool = world.GetPool<AxeViewRef>();

            foreach (int axe in filter)
            {
                if (viewRefPool.Has(axe))
                {
                    ref AxeViewRef viewRef = ref viewRefPool.Get(axe);

                    if (viewRef.Axe != null)
                    {
                        AxeEcsLink link = viewRef.Axe.GetComponent<AxeEcsLink>();

                        if (link != null)
                            link.Clear();

                        viewRef.Axe.DespawnByEcs();
                    }
                }

                world.DelEntity(axe);
            }
        }
    }
}