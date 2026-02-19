using Core.UI;
using UnityEngine;

namespace Game
{
    public class ProgressionSystem
    {
        private readonly UpgradeSystem _upgradeSystem;
        private readonly EnemySpawnerController _spawnerController;
        private readonly Wallet _wallet;
        private readonly ProgressConfig _config;

        private int _amountOfExperienceBeforeNextLevelUp;
        private int _currentPlayerLevel;

        public int MaxUpgrade => _amountOfExperienceBeforeNextLevelUp;
        public int CurrentExperience { get; private set; }

        public ProgressionSystem(UpgradeSystem upgradeSystem, EnemySpawnerController spawnerController, Wallet wallet, ProgressConfig config)
        {
            _upgradeSystem = upgradeSystem;
            _spawnerController = spawnerController;
            _wallet = wallet;
            _config = config;
            _amountOfExperienceBeforeNextLevelUp = _config.ExperienceBeforeLevelUp;
        }

        public void PickUpCrystal(Crystal crystal)
        {
            crystal.PickUp();
            if (_upgradeSystem.IsMaxUpgrades) return;

            CurrentExperience++;
            UIManager.GetScreen<HUD>().Refresh();

            if (CurrentExperience >= _amountOfExperienceBeforeNextLevelUp)
            {
                if (_currentPlayerLevel % _config.LevelUpStageStep == 0) _spawnerController.LevelUp();
                _wallet.AddCrystal();
                _currentPlayerLevel++;
                Time.timeScale = 0;
                UIManager.ShowWindow<UpgradeWindow>();
            }
        }

        public void Reset()
        {
            _amountOfExperienceBeforeNextLevelUp = _config.ExperienceBeforeLevelUp;
            CurrentExperience = 0;
            _currentPlayerLevel = 0;
        }
        
        public void UpgradeStats()
        {
            Time.timeScale = 1;
            if (_upgradeSystem.IsMaxUpgrades) return;
            CurrentExperience = 0;
            _amountOfExperienceBeforeNextLevelUp = (int)(_amountOfExperienceBeforeNextLevelUp * _config.ExperienceMultiplier);
            UIManager.GetScreen<HUD>().Refresh();
        }
    }
}