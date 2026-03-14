
using Contracts;
using Core.Pool;
using Core.UI;

namespace Game
{
    public class GameManager
    {
        private readonly PlayerPresenter _player;
        private readonly EnemySpawnerPresenter _spawnerPresenter;
        private readonly ProgressionPresenter _progressionPresenter;
        private readonly UpgradePresenter _upgradePresenter;
        private readonly Wallet _wallet;
        private readonly PoolManager _poolManager;
        private readonly IInput _input;
        private readonly IUIService _ui;
        
        public GameManager(PlayerPresenter player, EnemySpawnerPresenter spawnerPresenter, ProgressionPresenter progressionPresenter, UpgradePresenter upgradePresenter, Wallet wallet, PoolManager poolManager, IInput input, IUIService ui)
        {
            _player = player;
            _spawnerPresenter = spawnerPresenter;
            _progressionPresenter = progressionPresenter;
            _upgradePresenter = upgradePresenter;
            _wallet = wallet;
            _poolManager = poolManager;
            _input = input;
            _ui = ui;
            
            _player.OnDied += GameOver;
        }

        public void Dispose() => _player.OnDied -= GameOver;
        
        public void StartGame()
        {
            _input.SetActivate(true);
            _player.StartPlay();
            _spawnerPresenter.Start();
            _ui.ShowScreen<HUDView>();
        }

        public void RestartRun()
        {
            _poolManager.Reset();
            _wallet.Reset();
            _player.Restart();    
            _upgradePresenter.Reset();
            _spawnerPresenter.Reset();
            _progressionPresenter.Reset();
            _ui.ResetScreens();
            _ui.ShowScreen<MenuScreenView>();
        }
        
        private void GameOver()
        {
            _input.SetActivate(false);
            _spawnerPresenter.Stop();
            _ui.ShowScreen<LoseScreenView>();
        }
    }
}
