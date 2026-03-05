using System;
using Infrastructure;

namespace Application
{
    public class ProgressionService
    {
        private readonly WalletService _wallet;
        private readonly ProgressSettings _settings;
        private readonly GameTime _gameTime;
        private readonly EnemySpawnerController _spawner;
        private readonly UpgradeSystem _upgradeService;

        private int _amountOfExperienceBeforeNextLevelUp;
        private int _currentPlayerLevel;

        public event Action OnProgression;
        public event Action OnReachedGoal;
        
        public int MaxUpgrade => _amountOfExperienceBeforeNextLevelUp;
        public int CurrentExperience { get; private set; }

        public ProgressionService(WalletService wallet, ProgressSettings settings, GameTime gameTime, EnemySpawnerController spawner, UpgradeSystem upgradeService)
        {
            _wallet = wallet;
            _settings = settings;
            _gameTime = gameTime;
            _spawner = spawner;
            _upgradeService = upgradeService;
            _amountOfExperienceBeforeNextLevelUp = _settings.ExperienceBeforeLevelUp;
        }

        public void PickUpCrystal()
        {
            if (_upgradeService.IsMaxUpgrades) return;

            CurrentExperience++;
            OnProgression?.Invoke();

            if (CurrentExperience >= _amountOfExperienceBeforeNextLevelUp)
            {
                if (_currentPlayerLevel % _settings.LevelUpStageStep == 0) _spawner.LevelUp();
                _wallet.AddCrystal();
                _currentPlayerLevel++;
                _gameTime.Pause();
                OnReachedGoal?.Invoke();
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
            OnProgression?.Invoke();
        }
    }
}