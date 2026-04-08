using System;
using UniRx;

namespace Game
{
    public class ProgressionModel : IProgressionReadModel
    {
        private readonly ProgressConfig _config;
        private readonly IntReactiveProperty _currentExperience = new();
        private readonly IntReactiveProperty _maxUpgrade = new();
        private readonly Subject<Unit> _onLevelUp = new();
        private readonly Subject<Unit> _onLevelUpSpawner = new();

        private int _amountOfExperienceBeforeNextLevelUp;
        private int _currentPlayerLevel;

        public ProgressionModel(ProgressConfig config)
        {
            _config = config;
            _amountOfExperienceBeforeNextLevelUp = _config.ExperienceBeforeLevelUp;
            _maxUpgrade.Value = _amountOfExperienceBeforeNextLevelUp;
        }
        
        public IReadOnlyReactiveProperty<int> CurrentExperience => _currentExperience;
        public IReadOnlyReactiveProperty<int> MaxUpgrade => _maxUpgrade;
        public IObservable<Unit> OnLevelUp => _onLevelUp;
        public IObservable<Unit> OnLevelUpSpawner => _onLevelUpSpawner;
        
        public void IncreaseExperience()
        {
            _currentExperience.Value++;

            if (_currentExperience.Value >= _amountOfExperienceBeforeNextLevelUp)
            {
                if (_currentPlayerLevel % _config.LevelUpStageStep == 0)  _onLevelUpSpawner.OnNext(Unit.Default);
                _currentPlayerLevel++;
                _onLevelUp.OnNext(Unit.Default);
            }
        }

        public void UpgradeStats()
        {
            _currentExperience.Value = 0;
            _amountOfExperienceBeforeNextLevelUp = (int)(_amountOfExperienceBeforeNextLevelUp * _config.ExperienceMultiplier);
            _maxUpgrade.Value = _amountOfExperienceBeforeNextLevelUp;
        }

        public void Reset()
        {
            _amountOfExperienceBeforeNextLevelUp = _config.ExperienceBeforeLevelUp;
            _currentExperience.Value = 0;
            _currentPlayerLevel = 0;
        }
    }
}