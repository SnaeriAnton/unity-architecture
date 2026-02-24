using Domain;

namespace Application
{
    public class GameSessionService
    {
        private readonly Wallet _wallet;
        private readonly IInput _input;
        private readonly IUIRouter _uiRouter;
        private readonly IPlayerSession _player;
        private readonly IEnemySpawner _spawner;
        private readonly IProgression _progression;
        private readonly IPoolService _pool;
        private readonly IUpgradeService _upgrade;

        public GameSessionService(
            Wallet wallet, 
            IInput input, 
            IUIRouter uiRouter, 
            IPlayerSession player, 
            IEnemySpawner spawner, 
            IProgression progression, 
            IPoolService pool, 
            IUpgradeService upgrade
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