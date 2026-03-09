using Contracts;
using Core.Pool;
using Core.UI;

namespace Game
{
    public class GameManager
    {
        private readonly PlayerModel _player;
        private readonly EnemySpawnerController _spawnerController;
        private readonly ProgressionController _progressionController;
        private readonly UpgradeController _upgradeController;
        private readonly Wallet _wallet;
        private readonly PoolManager _poolManager;
        private readonly IInput _input;
        
        public GameManager(PlayerModel player, EnemySpawnerController spawnerController, ProgressionController progressionController, UpgradeController upgradeController, Wallet wallet, PoolManager poolManager, IInput input)
        {
            _player = player;
            _spawnerController = spawnerController;
            _progressionController = progressionController;
            _upgradeController = upgradeController;
            _wallet = wallet;
            _poolManager = poolManager;
            _input = input;
            
            _player.OnDied += GameOver;
        }

        public void Dispose() => _player.OnDied -= GameOver;
        
        public void StartGame()
        {
            _input.SetActivate(true);
            _player.StartPlay();
            _spawnerController.Start();
            UIManager.ShowScreen<HUD>();
        }

        public void RestartRun()
        {
            _poolManager.Reset();
            _wallet.Reset();
            _player.Restart();    
            _upgradeController.Init();
            _spawnerController.Reset();
            _progressionController.Reset();
            UIManager.ResetScreens();
            UIManager.ShowScreen<MenuScreen>();
        }
        
        private void GameOver()
        {
            _input.SetActivate(false);
            _spawnerController.Stop();
            UIManager.ShowScreen<LoseScreen>();
        }
    }
}
