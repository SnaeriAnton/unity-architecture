using System;
using Infrastructure;

namespace Application
{
    public class GameSessionService
    {
        private readonly WalletService _wallet;
        private readonly IInput _input;
        private readonly Player _player;
        private readonly EnemySpawnerController _spawner;
        private readonly ProgressionService _progression;
        private readonly PoolManager _pool;
        private readonly UpgradeSystem _upgrade;
        
        public event Action OnStartedGame;
        public event Action OnLost;
        public event Action OnRestartGame;
        public event Action OnResetValues;

        public GameSessionService(
            WalletService wallet, 
            IInput input,
            Player player, 
            EnemySpawnerController spawner, 
            ProgressionService progression, 
            PoolManager pool, 
            UpgradeSystem upgrade
            )
        {
            _wallet = wallet;
            _input = input;
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
            OnStartedGame?.Invoke();
        }

        public void GameOver()
        {
            _input.SetActivate(false);
            _spawner.Stop();
            OnLost?.Invoke();
        }

        public void RestartRun()
        {
            _pool.Reset();
            _wallet.Reset();
            _player.Restart();
            _upgrade.Init();
            _spawner.Reset();
            _progression.Reset();
            OnResetValues?.Invoke();
            OnRestartGame?.Invoke();
        }
    }
}