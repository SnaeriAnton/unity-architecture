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
        private GameEcsInstaller _gameEcsInstaller;
        private GameUiBridge _gameUiBridge;

        private bool IsMobile => Application.isMobilePlatform;

        public void StartGame()
        {
            Compose();
            Initialization();
        }

        private void Update() => _gameEcsInstaller?.Systems.Run(Time.deltaTime);
        private void OnDestroy() => Dispose();
        private void Dispose() { }

        private void Compose()
        {
            _inputRoot.Construct(IsMobile);
            _gameEcsInstaller = new();
            _gameEcsInstaller.Install(_playerAuthoring, _border, _inputRoot.Input);
            _pickupTrigger.Construct(_gameEcsInstaller.PickupApi);

            _gameUiBridge = new();
            GameTimeService gameTimeService = new();
            PoolManager poolManager = new();
            Wallet wallet = new(_gameConfig.StartCoinValues, _gameConfig.StartCrystalValues);
            Factory factory = new(poolManager, _playerAuthoring.Transform, _gameEcsInstaller.GameApi);
            EnemyDeathHandler enemyHandler = new(poolManager, _generatorData);
            EnemySpawnerController enemySpawnerController = new(_playerAuthoring.Transform, _generatorData, factory, _border);
            _gameEcsInstaller.InstallEnemySpawner(enemySpawnerController);
            _upgradeSystem = new(_weaponLevelUpsData, _playerLevelUpsData, factory, _gameEcsInstaller.WeaponApi, _gameConfig, wallet, _gameEcsInstaller.PlayerApi, _playerAuthoring.Transform, _gameEcsInstaller.HudEcsApi);
            ProgressionSystem progressionSystem = new(_upgradeSystem, wallet, _config, _gameEcsInstaller.EnemySpawnerApi, _gameUiBridge, _gameEcsInstaller.HudEcsApi, gameTimeService);
            PlayerLifecycleService playerLifecycleService = new(_gameEcsInstaller.PlayerApi, _playerDeathView, _gameEcsInstaller.WeaponApi);
            GameManager gameManager = new(enemySpawnerController, progressionSystem, _upgradeSystem, wallet, poolManager, _gameEcsInstaller.EnemySpawnerApi, playerLifecycleService, _inputRoot.Input, _gameUiBridge, _gameEcsInstaller.GameRuntimeEcsApi, _gameEcsInstaller.Diagnostics);
            PlayerDeathService playerDeathService = new(playerLifecycleService, _playerDeathView, gameManager);
            _uiRoot.Construct(progressionSystem, _upgradeSystem, wallet, gameManager, _gameEcsInstaller.PlayerHealthView, _gameEcsInstaller.PlayerShieldView);

            _gameEcsInstaller.InstallSystems(enemySpawnerController, enemyHandler, wallet, progressionSystem, playerDeathService, _gameUiBridge);
        }

        private void Initialization()
        {
            _upgradeSystem.Init();
            _gameUiBridge.ShowMenu();
        }
    }
}