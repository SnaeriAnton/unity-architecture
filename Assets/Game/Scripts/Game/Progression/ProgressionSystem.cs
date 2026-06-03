using UnityEngine;

namespace Game
{
    public class ProgressionSystem
    {
        private readonly UpgradeSystem _upgradeSystem;
        private readonly Wallet _wallet;
        private readonly ProgressConfig _config;
        private readonly EnemySpawnerApi _enemySpawnerApi;
        private readonly GameUiBridge _gameUiBridge;
        private readonly GameTimeService _gameTimeService;
        private readonly HudEcsApi _hudEcsApi;

        private int _amountOfExperienceBeforeNextLevelUp;
        private int _currentPlayerLevel;
        
        public ProgressionSystem(
            UpgradeSystem upgradeSystem, 
            Wallet wallet, 
            ProgressConfig config, 
            EnemySpawnerApi enemySpawnerApi, 
            GameUiBridge gameUiBridge, 
            HudEcsApi hudEcsApi,
            GameTimeService gameTimeService
            )
        {
            _upgradeSystem = upgradeSystem;
            _wallet = wallet;
            _config = config;
            _enemySpawnerApi = enemySpawnerApi;
            _gameUiBridge = gameUiBridge;
            _gameTimeService = gameTimeService;
            _hudEcsApi = hudEcsApi;
            _amountOfExperienceBeforeNextLevelUp = _config.ExperienceBeforeLevelUp;
        }

        public int MaxUpgrade => _amountOfExperienceBeforeNextLevelUp;
        public int CurrentExperience { get; private set; }

        public void PickUpCrystal()
        {
            if (_upgradeSystem.IsMaxUpgrades) return;

            CurrentExperience++;
            _hudEcsApi.RequestRefresh();

            if (CurrentExperience >= _amountOfExperienceBeforeNextLevelUp)
            {
                if (_currentPlayerLevel % _config.LevelUpStageStep == 0) _enemySpawnerApi.RequestNextStage();
                _wallet.AddCrystal();
                _currentPlayerLevel++;
                _gameTimeService.Pause();
                _gameUiBridge.ShowUpgradeWindow();
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
            _gameTimeService.Resume();
            if (_upgradeSystem.IsMaxUpgrades) return;
            CurrentExperience = 0;
            _amountOfExperienceBeforeNextLevelUp = (int)(_amountOfExperienceBeforeNextLevelUp * _config.ExperienceMultiplier);
            _hudEcsApi.RequestRefresh();
        }
    }
}