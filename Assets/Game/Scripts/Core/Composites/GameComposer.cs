using System.Collections.Generic;
using UnityEngine;
using Core.InputSystem;
using Core.Pool;
using Core.UI;
using Game;

namespace Core
{
    public class GameComposer : MonoBehaviour
    {
        private readonly List<ITickable> _tickables = new();

        [SerializeField] private List<WeaponLevelUpsData> _weaponLevelUpsData;
        [SerializeField] private PlayerLevelUpsData _playerLevelUpsData;
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private Player _player;
        [SerializeField] private Border _border;
        [SerializeField] private GeneratorData _generatorData;
        [SerializeField] private ProgressConfig _config;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private InputRoot _inputRoot;

        private EnemySpawnerController _enemySpawnerController;
        private ProgressionSystem _progressionSystem;
        private UpgradeSystem _upgradeSystem;
        private GameManager _gameManager;
        private Wallet _wallet;
        private Factory _factory;
        private PoolManager _poolManager;
        private EnemyDeathHandler _enemyHandler;

        private bool IsMobile => Application.isMobilePlatform;
        
        public void StartGame()
        {
            Compose();
            Initialization();
        }

        private void Update() => _tickables.ForEach(t => t.Tick());
        private void OnDestroy() => Dispose();
        private void Dispose() { }

        private void Compose()
        {
            _inputRoot.Construct(IsMobile);
            _poolManager = new();
            _wallet = new(_gameConfig.StartCoinValues, _gameConfig.StartCrystalValues);
            _factory = new(_poolManager, _player);
            _enemyHandler = new(_player, _poolManager, _generatorData);
            _enemySpawnerController = new(_player, _generatorData, _enemyHandler, _factory, _border);
            _upgradeSystem = new(_weaponLevelUpsData, _playerLevelUpsData, _factory, _player, _gameConfig, _wallet);
            
            _progressionSystem = new(_upgradeSystem, _enemySpawnerController, _wallet, _config);
            _gameManager = new(_player, _enemySpawnerController, _progressionSystem, _upgradeSystem, _wallet, _poolManager, _inputRoot.Input);
            _player.Construct(_wallet, _progressionSystem.PickUpCrystal, _gameManager.GameOver, _border, _inputRoot.Input);
            _uiRoot.Construct(_progressionSystem, _upgradeSystem, _wallet, _gameManager, _player);

            _tickables.Add(_enemySpawnerController);
        }

        private void Initialization()
        {
            _upgradeSystem.Init();
            UIManager.ShowScreen<MenuScreen>();
        }
    }
}