using System;

namespace Application
{
    public class ProgressionService : IProgressionReadModel, IProgressionCommands
    {
        private readonly IWallet _wallet;
        private readonly ProgressSettings _settings;
        private readonly IGameTime _gameTime;
        private readonly IUIRouter _uiRouter;
        private readonly IEnemySpawner _spawner;
        private readonly IUpgradeState _upgradeService;

        private int _amountOfExperienceBeforeNextLevelUp;
        private int _currentPlayerLevel;

        public ProgressionService(IWallet wallet, ProgressSettings settings, IGameTime gameTime, IUIRouter uiRouter, IEnemySpawner spawner, IUpgradeState upgradeService)
        {
            _wallet = wallet;
            _settings = settings;
            _gameTime = gameTime;
            _uiRouter = uiRouter;
            _spawner = spawner;
            _upgradeService = upgradeService;
            _amountOfExperienceBeforeNextLevelUp = _settings.ExperienceBeforeLevelUp;
        }
        
        public int MaxUpgrade => _amountOfExperienceBeforeNextLevelUp;
        public int CurrentExperience { get; private set; }

        public event Action OnPickUpCrystal;
        public event Action OnUpgradeStats;
        
        public void PickUpCrystal()
        {
            if (_upgradeService.IsMaxUpgrades) return;

            CurrentExperience++;
            OnPickUpCrystal?.Invoke();

            //if (CurrentExperience >= _amountOfExperienceBeforeNextLevelUp)
            if (CurrentExperience >= 2)
            {
                if (_currentPlayerLevel % _settings.LevelUpStageStep == 0) _spawner.LevelUp();
                _wallet.AddCrystal();
                _currentPlayerLevel++;
                _gameTime.Pause();
                _uiRouter.ShowUpgrade();
            }
        }

        public void Reset()
        {
            _amountOfExperienceBeforeNextLevelUp = _settings.ExperienceBeforeLevelUp;
            CurrentExperience = 0;
            _currentPlayerLevel = 0;
        }

        public void UpgradeStats()
        {
            _gameTime.Resume();
            if (_upgradeService.IsMaxUpgrades) return;
            CurrentExperience = 0;
            _amountOfExperienceBeforeNextLevelUp = (int)(_amountOfExperienceBeforeNextLevelUp * _settings.ExperienceMultiplier);
            OnUpgradeStats?.Invoke();
        }
    }
}