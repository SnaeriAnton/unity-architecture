using Contracts;
using Core.Pool;
using R3;

namespace Game
{
    public class GameManager
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly PlayerController _player;
        private readonly PlayerModel _playerModel;
        private readonly EnemySpawnerController _spawnerController;
        private readonly ProgressionController _progressionController;
        private readonly UpgradeController _upgradeController;
        private readonly Wallet _wallet;
        private readonly PoolManager _poolManager;
        private readonly GameFlowCoordinator _gameFlowCoordinator;
        private readonly IInput _input;
        
        public GameManager(
            PlayerController player, 
            PlayerModel playerModel,
            EnemySpawnerController spawnerController, 
            ProgressionController progressionController, 
            UpgradeController upgradeController, 
            Wallet wallet, 
            PoolManager poolManager, 
            GameFlowCoordinator gameFlowCoordinator,
            IInput input
            )
        {
            _player = player;
            _playerModel = playerModel;
            _spawnerController = spawnerController;
            _progressionController = progressionController;
            _upgradeController = upgradeController;
            _wallet = wallet;
            _poolManager = poolManager;
            _gameFlowCoordinator = gameFlowCoordinator;
            _input = input;
            
            _playerModel.OnDied.Subscribe(_ => GameOver()).AddTo(_disposable);
        }
        
        public void Dispose() => _disposable.Dispose();
        
        public void StartGame()
        {
            _input.SetActivate(true);
            _player.StartPlay();
            _spawnerController.Start();
            _gameFlowCoordinator.ShowHud();
        }

        public void RestartRun()
        {
            _gameFlowCoordinator.ResetUI();
            _poolManager.Reset();
            _wallet.Reset();
            _player.Restart();    
            _upgradeController.Reset();
            _spawnerController.Reset();
            _progressionController.Reset();
            _gameFlowCoordinator.ShowMenu();
        }
        
        private void GameOver()
        {
            _input.SetActivate(false);
            _spawnerController.Stop();
            _gameFlowCoordinator.ShowLose();
        }
    }
}
