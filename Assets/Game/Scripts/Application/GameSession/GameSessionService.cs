using Domain;

namespace Application
{
    public class GameSessionService : IGameSessionCommands
    {
        private readonly IWallet _wallet;
        private readonly IInput _input;
        private readonly IUIRouter _uiRouter;
        private readonly IPlayerSession _player;
        private readonly IEnemySpawner _spawner;
        private readonly IProgressionCommands _progression;
        private readonly IPoolService _pool;
        private readonly IUpgradeCommands _upgrade;

        public GameSessionService(
            IWallet wallet, 
            IInput input, 
            IUIRouter uiRouter, 
            IPlayerSession player, 
            IEnemySpawner spawner, 
            IProgressionCommands progression, 
            IPoolService pool, 
            IUpgradeCommands upgrade
            )
        {
            _wallet = wallet;
            _input = input;
            _uiRouter = uiRouter;
            _player = player;
            _spawner = spawner;
            _progression = progression;
            _pool = pool;
            _upgrade = upgrade;
        }

        public void StartGame()
        {
            _input.SetActivate(true);
            _player.StartPlay();
            _spawner.Start();
            _uiRouter.ShowHud();
        }

        public void GameOver()
        {
            _input.SetActivate(false);
            _spawner.Stop();
            _uiRouter.ShowLose();
        }

        public void RestartRun()
        {
            _pool.Reset();
            _wallet.Reset();
            _player.Restart();
            _upgrade.Init();
            _spawner.Reset();
            _progression.Reset();
            _uiRouter.ResetScreens();
            _uiRouter.ShowMenu();
        }
    }
}