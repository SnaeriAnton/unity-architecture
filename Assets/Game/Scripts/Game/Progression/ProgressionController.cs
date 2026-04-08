using Contracts;
using Core.UI;
using UniRx;

namespace Game
{
    public class ProgressionController : IProgression
    {
        private readonly CompositeDisposable _disposable = new();
        
        private readonly IUpgradeReadModel _upgrade;
        private readonly IEnemyStageProgression _spawnerController;
        private readonly IWalletWriter _wallet;
        private readonly IGamePauseService _gamePauseService;
        private readonly IUIService _ui;
        private readonly ProgressionModel _model;
        private readonly PlayerPickupHandler _playerPickupHandler;

        public ProgressionController(ProgressionModel model, PlayerPickupHandler playerPickupHandler, IUpgradeReadModel upgrade, IEnemyStageProgression spawnerController, IWalletWriter wallet, IGamePauseService gamePauseService, IUIService ui)
        {
            _playerPickupHandler = playerPickupHandler;
            _upgrade = upgrade;
            _spawnerController = spawnerController;
            _wallet = wallet;
            _model = model;
            _gamePauseService = gamePauseService;
            _ui = ui;

            _model.OnLevelUp.Subscribe(_ => LevelUp()).AddTo(_disposable);
            _model.OnLevelUpSpawner.Subscribe(_ => _spawnerController.LevelUp()).AddTo(_disposable);
            _playerPickupHandler.OnPickedUpCrystal.Subscribe(_ => PickUpCrystal()).AddTo(_disposable);
        }

        public void Dispose() => _disposable.Dispose();
        
        public void Reset() => _model.Reset();

        public void UpgradeStats()
        {
            _gamePauseService.Resume();
            if (_upgrade.IsMaxUpgrades) return;
            _model.UpgradeStats();
        }
        
        private void PickUpCrystal()
        {
            if (_upgrade.IsMaxUpgrades) return;

            _model.IncreaseExperience();
        }

        private void LevelUp()
        {
            _wallet.AddCrystal();
            _gamePauseService.Pause();
            _ui.ShowWindow<UpgradeWindowView>();
        }
    }
}