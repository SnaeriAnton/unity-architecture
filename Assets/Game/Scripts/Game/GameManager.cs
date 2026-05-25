using Contracts;
using Core.Pool;
using UnityEngine;

namespace Game
{
    public class GameManager
    {
        private readonly EnemySpawnerController _spawnerController;
        private readonly ProgressionSystem _progressionSystem;
        private readonly UpgradeSystem _upgradeSystem;
        private readonly Wallet _wallet;
        private readonly PoolManager _poolManager;
        private readonly PlayerLifecycleService _playerLifecycleService;
        private readonly EnemySpawnerApi _enemySpawnerApi;
        private readonly GameUiBridge _gameUiBridge;
        private readonly GameRuntimeEcsApi _gameRuntimeEcsApi;
        private readonly EcsDiagnostics _diagnostics;
        private readonly IInput _input;
        
        public GameManager(
            EnemySpawnerController spawnerController, 
            ProgressionSystem progressionSystem, 
            UpgradeSystem upgradeSystem, 
            Wallet wallet, 
            PoolManager poolManager, 
            EnemySpawnerApi enemySpawnerApi,
            PlayerLifecycleService playerLifecycleService, 
            IInput input,
            GameUiBridge gameUiBridge, 
            GameRuntimeEcsApi gameRuntimeEcsApi, 
            EcsDiagnostics diagnostics)
        {
            _spawnerController = spawnerController;
            _progressionSystem = progressionSystem;
            _upgradeSystem = upgradeSystem;
            _wallet = wallet;
            _poolManager = poolManager;
            _enemySpawnerApi = enemySpawnerApi;
            _playerLifecycleService = playerLifecycleService;
            _input = input;
            _gameUiBridge = gameUiBridge;
            _gameRuntimeEcsApi = gameRuntimeEcsApi;
            _diagnostics = diagnostics;
        }
        
        public void StartGame()
        {
            _input.SetActivate(true);
            _playerLifecycleService.Start();
            _spawnerController.Start();
            _gameUiBridge.ShowHUD();
        }

        public void GameOver()
        {
            _input.SetActivate(false);
            _spawnerController.Stop();
            _gameUiBridge.ShowLoseScreen();
        }

        public void RestartRun()
        {
            _gameRuntimeEcsApi.RequestCleanup();
            _poolManager.Reset();
            _wallet.Reset();
            _spawnerController.Reset();
            _enemySpawnerApi.ResetTimer();
            _playerLifecycleService.Restart();    
            _upgradeSystem.Init();
            _progressionSystem.Reset();
            _gameUiBridge.ResetScreens();
            _gameUiBridge.ShowMenu();
            _diagnostics.LogRuntimeState();
        }
    }
}
