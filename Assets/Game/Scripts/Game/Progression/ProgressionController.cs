using UnityEngine;

namespace Game
{
    public class ProgressionController : IProgression
    {
        private readonly UpgradeController _upgradeController;
        private readonly EnemySpawnerController _spawnerController;
        private readonly Wallet _wallet;
        private readonly ProgressionModel _model;
        private readonly PlayerView _player;

        public ProgressionController(PlayerView player, UpgradeController upgradeController, EnemySpawnerController spawnerController, Wallet wallet, ProgressConfig config)
        {
            _player = player;
            _upgradeController = upgradeController;
            _spawnerController = spawnerController;
            _wallet = wallet;
            _model = new(config);

            _model.OnLevelUp += LevelUp;
            _model.OnLevelUpSpawner += _spawnerController.LevelUp;
            _player.OnTouchedCrystal += PickUpCrystal;
        }
        
        public IProgressionReadModel Model => _model;

        public void Dispose()
        {
            _model.OnLevelUp -= LevelUp;
            _model.OnLevelUpSpawner -= _spawnerController.LevelUp;
            _player.OnTouchedCrystal -= PickUpCrystal;
        }
        
        public void Reset() => _model.Reset();

        public void UpgradeStats()
        {
            Time.timeScale = 1;
            if (_upgradeController.IsMaxUpgrades) return;
            _model.UpgradeStats();
        }
        
        private void PickUpCrystal(Crystal crystal)
        {
            crystal.PickUp();
            if (_upgradeController.IsMaxUpgrades) return;

            _model.IncreaseExperience();
        }

        private void LevelUp()
        {
            _wallet.AddCrystal();
            Time.timeScale = 0;
        }
    }
}