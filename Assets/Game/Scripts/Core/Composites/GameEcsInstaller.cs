using Contracts;

namespace Game
{
    public class GameEcsInstaller
    {
        private EcsWorld _world;
        private Entity _enemySpawnerEntity;
        private Entity _playerEntity;
        private PlayerInputSystem _playerInputSystem;

        public EcsSystems Systems { get; private set; }
        public PlayerHealthView PlayerHealthView { get; private set; }
        public GameApi GameApi { get; private set; }
        public PlayerApi PlayerApi { get; private set; }
        public PickupApi PickupApi { get; private set; }
        public EnemySpawnerApi EnemySpawnerApi { get; private set; }
        public WeaponApi WeaponApi { get; private set; }
        public HudEcsApi HudEcsApi { get; private set; }
        public PlayerShieldView PlayerShieldView { get; private set; }
        public PlayerWeaponRegistry PlayerWeaponRegistry { get; private set; }
        public GameRuntimeEcsApi GameRuntimeEcsApi { get; private set; }
        public EcsDiagnostics Diagnostics { get; private set; }

        public void Install(PlayerAuthoring playerAuthoring, Border border, IInput input)
        {
            _world = new();
            Systems = new(_world);
            HudEcsApi = new(Systems.CommandBuffer);

            EnemyFactory enemyFactory = new(Systems.CommandBuffer);
            AxeFactory axeFactory = new(Systems.CommandBuffer);
            ProjectileFactory projectileFactory = new(Systems.CommandBuffer);
            GameRuntimeEcsApi = new(Systems.CommandBuffer);
            PickupApi = new(_world, Systems.CommandBuffer);
            PlayerWeaponRegistry = new();
            Diagnostics = new EcsDiagnostics(_world);

            GameApi = new(_world, enemyFactory, axeFactory, Systems.CommandBuffer, projectileFactory, new WeaponHitboxFactory(Systems.CommandBuffer), new ProjectileWeaponFactory(Systems.CommandBuffer), new OrbitWeaponFactory(Systems.CommandBuffer));
            PlayerFactory playerFactory = new(_world, Systems.CommandBuffer);
            _playerEntity = playerFactory.CreatePlayer(playerAuthoring.Transform, border, playerAuthoring.Speed, playerAuthoring.IFramesDuration);
            _playerInputSystem = new(_world, _playerEntity, input);
            PlayerApi = new(_world, _playerEntity, _playerInputSystem, Systems.CommandBuffer);
            PlayerHealthView = new(_world, _playerEntity);
            WeaponApi = new(Systems.CommandBuffer, PlayerWeaponRegistry);
            PlayerShieldView = new(_world, _playerEntity);
        }

        public void InstallEnemySpawner(EnemySpawnerController spawnerController)
        {
            _enemySpawnerEntity = _world.CreateEntity();
            _world.Add(_enemySpawnerEntity, new EnemySpawnTimer { TimeLeft = 0f });
            EnemySpawnerApi = new(_world, _enemySpawnerEntity, spawnerController, Systems.CommandBuffer);
        }
        
        public void InstallSystems(
            EnemySpawnerController spawnerController,
            EnemyDeathHandler enemyDeathHandler,
            Wallet wallet,
            ProgressionSystem progressionSystem,
            PlayerDeathService playerDeathService,
            GameUiBridge  uiBridge)
        {
            Systems.Add(new GameRuntimeCleanupSystem(_world, Systems.CommandBuffer));
#if UNITY_EDITOR
            Systems.Add(new GameRuntimeCleanupValidationSystem(_world, Systems.CommandBuffer, Diagnostics));
#endif
            Systems.Add(_playerInputSystem);
            Systems.Add(new PlayerShieldBlockSystem(_world, Systems.CommandBuffer));
            Systems.Add(new PlayerShieldSetSystem(_world, Systems.CommandBuffer));
            Systems.Add(new PlayerDamageSystem(_world, Systems.CommandBuffer));
            Systems.Add(new PlayerDeathSystem(_world, playerDeathService.HandleDeath, Systems.CommandBuffer));
            Systems.Add(new PlayerMovementSystem(_world));
            Systems.Add(new PlayerInvulnerabilityTimerSystem(_world));

            Systems.Add(new EnemySpawnStageSystem(_world, spawnerController, Systems.CommandBuffer));
            Systems.Add(new EnemySpawnTimerSystem(_world, () => spawnerController.CanSpawn, () => spawnerController.CurrentSpawnInterval, Systems.CommandBuffer));
            Systems.Add(new EnemySpawnSystem(_world, spawnerController, Systems.CommandBuffer));

            Systems.Add(new EnemyDamageSystem(_world, Systems.CommandBuffer));
            Systems.Add(new EnemySuicideAttackSystem(_world, Systems.CommandBuffer));
            Systems.Add(new EnemyDeathSystem(_world, Systems.CommandBuffer));
            Systems.Add(new PlayerShieldRechargeSystem(_world, Systems.CommandBuffer));
            Systems.Add(new EnemyDropSystem(_world, enemyDeathHandler, Systems.CommandBuffer));
            Systems.Add(new EnemyDespawnSystem(_world, enemyDeathHandler, Systems.CommandBuffer));

            Systems.Add(new EnemyMeleeAttackSystem(_world, Systems.CommandBuffer));
            Systems.Add(new EnemyAxeAttackSystem(_world));
            Systems.Add(new EnemyFollowPlayerSystem(_world));

            Systems.Add(new AxeMovementSystem(_world));
            Systems.Add(new AxeHitPlayerSystem(_world, Systems.CommandBuffer));
            Systems.Add(new AxeLifetimeSystem(_world, Systems.CommandBuffer));
            Systems.Add(new AxeDespawnSystem(_world, Systems.CommandBuffer));
            Systems.Add(new ProjectileMovementSystem(_world));
            Systems.Add(new ProjectileHitEnemySystem(_world, Systems.CommandBuffer));
            Systems.Add(new ProjectileLifetimeSystem(_world, Systems.CommandBuffer));
            Systems.Add(new ProjectileDespawnSystem(_world, Systems.CommandBuffer));
            Systems.Add(new ProjectileWeaponAttackSystem(_world));
            Systems.Add(new ProjectileWeaponCooldownSetSystem(_world, Systems.CommandBuffer));
            Systems.Add(new WeaponHitboxHitEnemySystem(_world, Systems.CommandBuffer));
            Systems.Add(new WeaponHitboxDamageSetSystem(_world, Systems.CommandBuffer));
            Systems.Add(new OrbitWeaponRotationSetSystem(_world, Systems.CommandBuffer));
            Systems.Add(new OrbitWeaponRotationSystem(_world));

            Systems.Add(new CoinPickupSystem(_world, wallet, Systems.CommandBuffer));
            Systems.Add(new CrystalPickupSystem(_world, Systems.CommandBuffer, progressionSystem.PickUpCrystal));

            Systems.Add(new PlayerWeaponResetSystem(_world, Systems.CommandBuffer, PlayerWeaponRegistry));
            Systems.Add(new PlayerWeaponUpgradeSystem(_world, Systems.CommandBuffer, PlayerWeaponRegistry));
            
            Systems.Add(new HudRefreshSystem(_world, Systems.CommandBuffer, uiBridge));
            
            Systems.Add(new PlayerViewSyncSystem(_world));
            Systems.Add(new EnemyViewSyncSystem(_world));
            Systems.Add(new AxeViewSyncSystem(_world));
            Systems.Add(new ProjectileViewSyncSystem(_world));
            Systems.Add(new OrbitWeaponViewSyncSystem(_world));
        }
    }
}