using Shared;

namespace GameFlow
{
    public class GameManager
    {
        private readonly IPlayerLifecycle _player;
        private readonly IEnemySpawner _enemySpawner;
        private readonly IProgression _progression;
        private readonly IUpgrade _upgrade;
        private readonly IWallet _wallet;
        private readonly IObjectPool _pool;
        private readonly IInput _input;
        
        public GameManager(IPlayerLifecycle player, IEnemySpawner enemySpawner, IProgression progression, IUpgrade upgrade, IWallet wallet, IObjectPool pool, IInput input)
        {
            _player = player;
            _enemySpawner = enemySpawner;
            _progression = progression;
            _upgrade = upgrade;
            _wallet = wallet;
            _pool = pool;
            _input = input;
            
            GameEvents.GameFlow.GameStarted += StartGame;
            GameEvents.GameFlow.GameRestart += RestartRun;
        }

        public void Dispose()
        {
            GameEvents.GameFlow.GameStarted -= StartGame;
            GameEvents.GameFlow.GameRestart -= RestartRun;
        }
        
        public void GameOver()
        {
            _input.SetActivate(false);
            _enemySpawner.Stop();
            GameEvents.GameFlow.RaiseScreenRequested(GameScreen.Lose);
        }
        
        private void StartGame()
        {
            _input.SetActivate(true);
            _player.StartPlay();
            _enemySpawner.Start();
            GameEvents.GameFlow.RaiseScreenRequested(GameScreen.Hud);
        }

        private void RestartRun()
        {
            GameEvents.GameFlow.RaiseGameRestarted();
            _pool.Reset();
            _wallet.Reset();
            _player.Restart();    
            _upgrade.Init();
            _enemySpawner.Reset();
            _progression.Reset();
            GameEvents.GameFlow.RaiseScreenRequested(GameScreen.Menu);
        }
    }
}
