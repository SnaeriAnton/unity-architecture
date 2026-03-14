using System;

namespace Game
{
    public class ProgressionModel : IProgressionReadModel
    {
        private readonly ProgressConfig _config;

        private int _amountOfExperienceBeforeNextLevelUp;
        private int _currentPlayerLevel;

        public ProgressionModel(ProgressConfig config)
        {
            _config = config;
            _amountOfExperienceBeforeNextLevelUp = _config.ExperienceBeforeLevelUp;
        }
        
        public int MaxUpgrade => _amountOfExperienceBeforeNextLevelUp;
        public int CurrentExperience { get; private set; }

        public event Action OnLevelUp;
        public event Action OnLevelUpSpawner;
        public event Action OnProgressChanged;
        
        public void IncreaseExperience()
        {
            CurrentExperience++;
            OnProgressChanged?.Invoke();

            if (CurrentExperience >= _amountOfExperienceBeforeNextLevelUp)
            {
                if (_currentPlayerLevel % _config.LevelUpStageStep == 0) OnLevelUpSpawner?.Invoke();
                _currentPlayerLevel++;
                OnLevelUp?.Invoke();
            }
        }

        public void UpgradeStats()
        {
            CurrentExperience = 0;
            _amountOfExperienceBeforeNextLevelUp = (int)(_amountOfExperienceBeforeNextLevelUp * _config.ExperienceMultiplier);
            OnProgressChanged?.Invoke();
        }

        public void Reset()
        {
            _amountOfExperienceBeforeNextLevelUp = _config.ExperienceBeforeLevelUp;
            CurrentExperience = 0;
            _currentPlayerLevel = 0;
        }
    }
}