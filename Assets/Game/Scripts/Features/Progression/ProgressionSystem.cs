using Shared;

namespace Progression
{
    public class ProgressionSystem : IProgression
    {
        private readonly ProgressConfig _config;
        private readonly IUpgrade _upgrade;
        private readonly IEnemySpawner _enemySpawner;
        private readonly IWallet _wallet;

        private int _amountOfExperienceBeforeNextLevelUp;
        private int _currentPlayerLevel;
        private int _currentExperience;

        private int MaxUpgrade => _amountOfExperienceBeforeNextLevelUp;

        public ProgressionSystem( ProgressConfig config, IUpgrade upgrade, IEnemySpawner enemySpawner, IWallet wallet)
        {
            _upgrade = upgrade;
            _enemySpawner = enemySpawner;
            _wallet = wallet;
            _config = config;
            _amountOfExperienceBeforeNextLevelUp = _config.ExperienceBeforeLevelUp;
            GameEvents.Progression.UpgradeCompleted += UpgradeStats;
            GameEvents.Loot.CrystalPicked += PickUpCrystal;
        }

        public void Dispose()
        {
            GameEvents.Progression.UpgradeCompleted -= UpgradeStats;
            GameEvents.Loot.CrystalPicked -= PickUpCrystal;
        }

        public void Reset()
        {
            _amountOfExperienceBeforeNextLevelUp = _config.ExperienceBeforeLevelUp;
            _currentExperience = 0;
            _currentPlayerLevel = 0;
        }

        private void UpgradeStats()
        {
            GameEvents.Progression.RaisePauseRequested(false);
            if (_upgrade.IsMaxUpgrades) return;
            _currentExperience = 0;
            _amountOfExperienceBeforeNextLevelUp = (int)(_amountOfExperienceBeforeNextLevelUp * _config.ExperienceMultiplier);
            GameEvents.Progression.RaiseExpChanged(_currentExperience, MaxUpgrade);
        }
        
        private void PickUpCrystal()
        {
            if (_upgrade.IsMaxUpgrades) return;

            _currentExperience++;
            GameEvents.Progression.RaiseExpChanged(_currentExperience, MaxUpgrade);

            if (_currentExperience >= _amountOfExperienceBeforeNextLevelUp)
            {
                if (_currentPlayerLevel % _config.LevelUpStageStep == 0) _enemySpawner.LevelUp();
                _wallet.AddCrystal();
                _currentPlayerLevel++;
                GameEvents.Progression.RaisePauseRequested(true);
                GameEvents.Progression.RaiseUpgradeMenuRequested();
            }
        }
    }
}