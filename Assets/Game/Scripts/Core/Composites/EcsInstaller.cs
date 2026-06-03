using Contracts;
using Leopotam.EcsLite;

namespace Game
{
    public class EcsInstaller
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        private PlayerInputSystem _playerInputSystem;

        public ProjectileApi ProjectileApi { get; private set; }
        public ProjectileWeaponApi ProjectileWeaponApi { get; private set; }
        public WeaponHitboxApi WeaponHitboxApi { get; private set; }
        public OrbitWeaponApi OrbitWeaponApi { get; private set; }
        public LeoEnemyApi LeoEnemyApi { get; private set; }
        public AxeApi AxeApi { get; private set; }
        public EnemySpawnerApi EnemySpawnerApi { get; private set; }
        public PlayerApi PlayerApi { get; private set; }
        public PickupApi PickupApi { get; private set; }
        public WeaponApi WeaponApi { get; private set; }
        public PlayerWeaponRegistry PlayerWeaponRegistry { get; private set; }
        public GameRuntimeEcsApi GameRuntimeEcsApi { get; private set; }
        public HudEcsApi HudEcsApi { get; private set; }
        public PlayerHealthView PlayerHealthView { get; private set; }
        public PlayerShieldView PlayerShieldView { get; private set; }
        public EcsWorld World => _world;

        public void Install(IInput input)
        {
            _world = new();
            _systems = new(_world);
            ProjectileApi = new(_world);
            ProjectileWeaponApi = new(_world);
            WeaponHitboxApi = new(_world);
            OrbitWeaponApi = new(_world);
            AxeApi = new(_world);
            LeoEnemyApi = new(_world);
            EnemySpawnerApi = new(_world);
            GameRuntimeEcsApi = new(_world);
            PlayerApi = new(_world);
            PickupApi = new(_world);
            HudEcsApi = new(_world);
            PlayerHealthView = new(PlayerApi, _world);
            PlayerWeaponRegistry = new();
            WeaponApi = new(_world, PlayerWeaponRegistry);
            _playerInputSystem = new(PlayerApi, input);
            PlayerShieldView = new(PlayerApi, _world);
            PlayerApi.SetInputSystem(_playerInputSystem);
            EnemySpawnerApi.RegisterSpawner();
        }

        public void InstallSystems(
            Wallet wallet,
            EnemyDeathHandler enemyDeathHandler,
            EnemySpawnerController spawnerController,
            PlayerDeathService playerDeathService,
            GameUiBridge uiBridge,
            ProgressionSystem progressionSystem)
        {
            _systems
                .Add(new GameRuntimeCleanupSystem())
                .Add(new PlayerWeaponResetSystem(PlayerWeaponRegistry, PlayerApi))
                .Add(new PlayerWeaponUpgradeSystem(PlayerWeaponRegistry, PlayerApi))
                .Add(new PlayerShieldSetSystem(PlayerApi))
                .Add(new WeaponHitboxDamageSetSystem())
                .Add(new ProjectileWeaponCooldownSetSystem())
                .Add(new OrbitWeaponRotationSetSystem())
                .Add(_playerInputSystem)
                .Add(new PlayerMovementSystem())
                .Add(new EnemySpawnStageSystem(spawnerController))
                .Add(new EnemySpawnTimerSystem(PlayerApi, spawnerController))
                .Add(new EnemySpawnSystem(spawnerController))
                .Add(new ProjectileWeaponAttackSystem(PlayerApi))
                .Add(new EnemyAxeAttackSystem(PlayerApi))
                .Add(new OrbitWeaponRotationSystem(PlayerApi))
                .Add(new EnemyFollowPlayerSystem(PlayerApi))
                .Add(new AxeMovementSystem())
                .Add(new AxeRotationSystem())
                .Add(new ProjectileMovementSystem())
                .Add(new AxeLifetimeSystem())
                .Add(new ProjectileLifetimeSystem())
                .Add(new ProjectileHitEnemySystem())
                .Add(new WeaponHitboxHitEnemySystem())
                .Add(new AxeHitPlayerSystem(PlayerApi))
                .Add(new EnemyMeleeAttackSystem(PlayerApi))
                .Add(new EnemySuicideAttackSystem(PlayerApi))
                .Add(new PlayerShieldBlockSystem())
                .Add(new PlayerDamageSystem())
                .Add(new PlayerDeathSystem(playerDeathService))
                .Add(new PlayerInvulnerabilityTimerSystem())
                .Add(new EnemyDamageSystem())
                .Add(new EnemyDeathSystem())
                .Add(new PlayerShieldRechargeSystem(PlayerApi))
                .Add(new EnemyDropSystem(enemyDeathHandler))
                .Add(new EnemyDespawnSystem(enemyDeathHandler))
                .Add(new AxeDespawnSystem())
                .Add(new ProjectileDespawnSystem())
                .Add(new CoinPickupSystem(wallet))
                .Add(new CrystalPickupSystem(progressionSystem.PickUpCrystal))
                .Add(new HudRefreshSystem(uiBridge))
                .Add(new EnemyViewSyncSystem())
                .Add(new AxeViewSyncSystem())
                .Add(new ProjectileViewSyncSystem())
                .Add(new OrbitWeaponViewSyncSystem())
                .Add(new PlayerViewSyncSystem());

            _systems.Init();
        }

        public void Run() => _systems?.Run();

        public void Dispose()
        {
            _systems?.Destroy();
            _world?.Destroy();

            _systems = null;
            _world = null;
        }
    }
}