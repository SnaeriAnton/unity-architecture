using System.Collections.Generic;
using UnityEngine;
using Core.InputSystem;
using Core.Pool;
using Game;

namespace Core
{
    public class GameComposer : MonoBehaviour
    {
        [SerializeField] private List<WeaponLevelUpsData> _weaponLevelUpsData;
        [SerializeField] private PlayerLevelUpsData _playerLevelUpsData;
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private Border _border;
        [SerializeField] private GeneratorData _generatorData;
        [SerializeField] private ProgressConfig _config;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private InputRoot _inputRoot;
        [SerializeField] private PlayerPickupTrigger _pickupTrigger;
        [SerializeField] private PlayerDeathView _playerDeathView;
        [SerializeField] private PlayerAuthoring _playerAuthoring;

        private UpgradeSystem _upgradeSystem;
        private GameUiBridge _gameUiBridge;
        private EcsInstaller _ecsInstaller;

        private bool IsMobile => Application.isMobilePlatform;

        public void StartGame()
        {
            Compose();
            Initialization();
        }

        private void Update() => _ecsInstaller?.Run();

        private void OnDestroy() => Dispose();
        private void Dispose() => _ecsInstaller?.Dispose();

        private void Compose()
        {
            _inputRoot.Construct(IsMobile);
            
            _ecsInstaller = new ();
            _ecsInstaller.Install(_inputRoot.Input);
            _ecsInstaller.PlayerApi.RegisterPlayer(_playerAuthoring.Transform, _border, _playerAuthoring.Speed, _playerAuthoring.IFramesDuration);

            _pickupTrigger.Construct(_ecsInstaller.PickupApi);

            _gameUiBridge = new();
            GameTimeService gameTimeService = new();
            PoolManager poolManager = new();
            Wallet wallet = new(_gameConfig.StartCoinValues, _gameConfig.StartCrystalValues);
            Factory factory = new(poolManager, _playerAuthoring.Transform, _ecsInstaller.ProjectileApi, _ecsInstaller.ProjectileWeaponApi, _ecsInstaller.WeaponHitboxApi, _ecsInstaller.OrbitWeaponApi, _ecsInstaller.AxeApi, _ecsInstaller.LeoEnemyApi);
            EnemyDeathHandler enemyHandler = new(poolManager, _generatorData);
            EnemySpawnerController enemySpawnerController = new(_playerAuthoring.Transform, _generatorData, factory, _border);
            _upgradeSystem = new(_weaponLevelUpsData, _playerLevelUpsData, factory, _ecsInstaller.WeaponApi, _gameConfig, wallet, _ecsInstaller.PlayerApi, _playerAuthoring.Transform, _ecsInstaller.HudEcsApi);
            ProgressionSystem progressionSystem = new(_upgradeSystem, wallet, _config, _ecsInstaller.EnemySpawnerApi, _gameUiBridge, _ecsInstaller.HudEcsApi, gameTimeService);
            PlayerLifecycleService playerLifecycleService = new(_ecsInstaller.PlayerApi, _playerDeathView, _ecsInstaller.WeaponApi);
            GameManager gameManager = new(enemySpawnerController, progressionSystem, _upgradeSystem, wallet, poolManager, _ecsInstaller.EnemySpawnerApi, playerLifecycleService, _inputRoot.Input, _gameUiBridge, _ecsInstaller.GameRuntimeEcsApi);
            PlayerDeathService playerDeathService = new(playerLifecycleService, _playerDeathView, gameManager);
            _uiRoot.Construct(progressionSystem, _upgradeSystem, wallet, gameManager, _ecsInstaller.PlayerHealthView, _ecsInstaller.PlayerShieldView);

            _ecsInstaller.InstallSystems(wallet, enemyHandler, enemySpawnerController, playerDeathService, _gameUiBridge, progressionSystem);
        }

        private void Initialization()
        {
            _upgradeSystem.Init();
            _gameUiBridge.ShowMenu();
        }
    }
}