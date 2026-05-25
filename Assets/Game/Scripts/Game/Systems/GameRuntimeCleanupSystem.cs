namespace Game
{
    public class GameRuntimeCleanupSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly CommandBuffer _commandBuffer;

        public GameRuntimeCleanupSystem(EcsWorld world, CommandBuffer commandBuffer)
        {
            _world = world;
            _commandBuffer = commandBuffer;
        }

        public void Run(float deltaTime)
        {
            foreach (Entity request in _world.Filter().With<GameRuntimeCleanupRequest>())
            {
                ClearRuntimeRequests();

                foreach (Entity entity in _world.Filter().With<GameRuntimeTag>())
                {
                    ClearLinks(entity);
                    _commandBuffer.DestroyEntity(entity);
                }

                Entity validationRequest = _commandBuffer.CreateEntity();
                _commandBuffer.Add(validationRequest, new GameRuntimeCleanupValidationRequest());

                _commandBuffer.DestroyEntity(request);
            }
        }

        private void ClearLinks(Entity entity)
        {
            if (_world.Has<EnemyMonoRef>(entity))
            {
                EnemyMonoRef enemyRef = _world.Get<EnemyMonoRef>(entity);

                if (enemyRef.Enemy.TryGetComponent(out EnemyEcsLink enemyLink))
                    enemyLink.Clear();
            }

            if (_world.Has<AxeViewRef>(entity))
            {
                AxeViewRef axeRef = _world.Get<AxeViewRef>(entity);

                if (axeRef.Axe.TryGetComponent(out AxeEcsLink axeLink))
                    axeLink.Clear();
            }

            if (_world.Has<ProjectileViewRef>(entity))
            {
                ProjectileViewRef projectileRef = _world.Get<ProjectileViewRef>(entity);

                if (projectileRef.Link != null)
                    projectileRef.Link.Clear();
            }

            if (_world.Has<WeaponHitboxViewRef>(entity))
            {
                WeaponHitboxViewRef hitboxRef = _world.Get<WeaponHitboxViewRef>(entity);

                if (hitboxRef.Transform != null &&
                    hitboxRef.Transform.TryGetComponent(out WeaponHitboxEcsLink hitboxLink))
                {
                    hitboxLink.Clear();
                }
            }
        }
        
        private void ClearRuntimeRequests()
        {
            DestroyAll<DamageRequest>();
            DestroyAll<CoinPickupRequest>();
            DestroyAll<CrystalPickupRequest>();

            DestroyAll<EnemyDamageRequest>();
            DestroyAll<EnemySpawnStageNextRequest>();
            DestroyAll<EnemySpawnRequest>();
            DestroyAll<EnemyDropRequest>();
            DestroyAll<EnemyDespawnRequest>();
            DestroyAll<EnemyKilledRequest>();

            DestroyAll<AxeDespawnRequest>();

            DestroyAll<ProjectileDespawnRequest>();
            DestroyAll<WeaponHitboxHitEnemyRequest>();
            DestroyAll<WeaponHitboxDamageSetRequest>();

            DestroyAll<ProjectileWeaponCooldownSetRequest>();
            DestroyAll<OrbitWeaponRotationSetRequest>();
            
            DestroyAll<WeaponAddRequest>();
            DestroyAll<WeaponStatsSetRequest>();
            DestroyAll<ProjectileHitEnemyTag>();
            DestroyAll<ProjectileHitEnemyTarget>();
            DestroyAll<AxeHitPlayerTag>();
        }
        
        private void DestroyAll<T>() where T : struct
        {
            foreach (Entity entity in _world.Filter().With<T>())
                _commandBuffer.DestroyEntity(entity);
        }
    }
}