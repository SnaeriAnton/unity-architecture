namespace Game
{
    public class PlayerDeathService
    {
        private readonly PlayerLifecycleService _playerLifecycleService;
        private readonly PlayerDeathView _playerDeathView;
        private readonly GameManager _gameManager;

        public PlayerDeathService(PlayerLifecycleService playerLifecycleService, PlayerDeathView playerDeathView, GameManager gameManager)
        {
            _playerLifecycleService = playerLifecycleService;
            _playerDeathView = playerDeathView;
            _gameManager = gameManager;
        }

        public void HandleDeath()
        {
            _playerLifecycleService.Stop();
            _playerDeathView.ShowDeath();
            _gameManager.GameOver();
        }
    }
}