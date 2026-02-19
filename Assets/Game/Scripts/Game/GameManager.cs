using Contracts;
using Core.Pool;
using Core.UI;

namespace Game
{
    public class GameManager
    {
        private readonly Player _player;
        private readonly EnemySpawnerController _spawnerController;
        private readonly ProgressionSystem _progressionSystem;
        private readonly UpgradeSystem _upgradeSystem;
        private readonly Wallet _wallet;
        private readonly PoolManager _poolManager;
        private readonly IInput _input;
        
        public GameManager(Player player, EnemySpawnerController spawnerController, ProgressionSystem progressionSystem, UpgradeSystem upgradeSystem, Wallet wallet, PoolManager poolManager, IInput input)
        {
            _player = player;
            _spawnerController = spawnerController;
            _progressionSystem = progressionSystem;
            _upgradeSystem = upgradeSystem;
            _wallet = wallet;
            _poolManager = poolManager;
            _input = input;
        }
        
        public void StartGame()
        {
            _input.SetActivate(true);
            _player.StartPlay();
            _spawnerController.Start();
            UIManager.ShowScreen<HUD>();
        }

        public void GameOver()
        {
            _input.SetActivate(false);
            _spawnerController.Stop();
            UIManager.ShowScreen<LoseScreen>();
        }

        public void RestartRun()
        {
            _poolManager.Reset();
            _wallet.Reset();
            _player.Restart();    
            _upgradeSystem.Init();
            _spawnerController.Reset();
            _progressionSystem.Reset();
            UIManager.ResetScreens();
            UIManager.ShowScreen<MenuScreen>();
        }
    }
}
