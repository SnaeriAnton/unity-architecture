using Contracts;
using Core.GSystem;
using Core.Pool;
using Core.UI;

namespace Game
{
    public class GameManager : IGameManager
    {
        private readonly EnemySpawnerController _spawnerController;
        private readonly ProgressionSystem _progressionSystem;
        private readonly UpgradeSystem _upgradeSystem;
        
        private Player _player;
        private PoolManager _poolManager;
        private Wallet _wallet;
        private IInput _input;
        
        public GameManager(EnemySpawnerController spawnerController, ProgressionSystem progressionSystem, UpgradeSystem upgradeSystem)
        {
            _spawnerController = spawnerController;
            _progressionSystem = progressionSystem;
            _upgradeSystem = upgradeSystem;
        }
        
        private Player Player => _player ??= G.Main.Resolve<Player>();
        private PoolManager Pool => _poolManager ??= G.Main.Resolve<PoolManager>();
        private Wallet Wallet => _wallet ??= G.Main.Resolve<Wallet>();
        private IInput Input => _input ??= G.Main.Resolve<IInput>();
        
        public void StartGame()
        {
            Input.SetActivate(true);
            Player.StartPlay();
            _spawnerController.Start();
            G.Main.Resolve<IUIService>().ShowScreen<HUD>();
        }

        public void GameOver()
        {
            Input.SetActivate(false);
            _spawnerController.Stop();
            G.Main.Resolve<IUIService>().ShowScreen<LoseScreen>();
        }

        public void RestartRun()
        {
            Pool.Reset();
            Wallet.Reset();
            Player.Restart();    
            _upgradeSystem.Init();
            _spawnerController.Reset();
            _progressionSystem.Reset();
            IUIService ui = G.Main.Resolve<IUIService>();
            ui.ResetScreens();
            ui.ShowScreen<MenuScreen>();
        }
    }
}
