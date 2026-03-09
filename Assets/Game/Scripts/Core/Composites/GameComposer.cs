using System.Collections.Generic;
using UnityEngine;
using Contracts;
using Core.InputSystem;
using Core.Pool;
using Core.UI;
using Game;
using UnityEditor;

namespace Core
{
    public class GameComposer : MonoBehaviour
    {
        private readonly List<ITickable> _tickables = new();

        [SerializeField] private List<WeaponLevelUpsData> _weaponLevelUpsData;
        [SerializeField] private PlayerLevelUpsData _playerLevelUpsData;
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private Border _border;
        [SerializeField] private GeneratorData _generatorData;
        [SerializeField] private ProgressConfig _config;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private InputRoot _inputRoot;
        [SerializeField] private PlayerStats _playerStats;

        private PlayerController _playerController;
        private PlayerModel _playerModel;
        private Wallet _wallet;
        private PoolManager _poolManager;
        private EnemySpawnerController _enemySpawnerController;
        private ProgressionController _progressionController;
        private UpgradeController _upgradeController;
        private GameManager _gameManager;
        private Factory _factory;
        private EnemyDeathHandler _enemyHandler;

        private bool IsMobile => Application.isMobilePlatform;

        public void StartGame()
        {
            Compose();
            Initialization();
        }

        private void Update() => _tickables.ForEach(t => t.Tick());
        private void OnDestroy() => Dispose();

        private void Dispose()
        {
            _playerView.OnTouchedCoin -= AddCoin;
            _uiRoot.Dispose();
            _gameManager.Dispose();
            _progressionController.Dispose();
            _playerController.Dispose();
            _playerView.Dispose();
            _playerModel.Dispose();
        }

        private void Compose()
        {
            _inputRoot.Construct(IsMobile);
            _poolManager = new();
            _wallet = new(_gameConfig.StartCoinValues, _gameConfig.StartCrystalValues);
            _playerModel = new(_playerStats.IFramesDuration);
            _playerView.Construct(_playerModel);
            _playerController = new(_playerView.transform, _playerModel, _border, _inputRoot.Input, _playerStats.Speed);
            _factory = new(_poolManager, _playerView);
            _enemyHandler = new(_playerModel, _poolManager, _generatorData);
            _enemySpawnerController = new(_playerView, _generatorData, _enemyHandler, _factory, _border);
            _upgradeController = new(_weaponLevelUpsData, _playerLevelUpsData, _factory, _playerModel, _gameConfig, _wallet);
            _progressionController = new(_playerView, _upgradeController, _enemySpawnerController, _wallet, _config);
            _gameManager = new(_playerModel, _enemySpawnerController, _progressionController, _upgradeController, _wallet, _poolManager, _inputRoot.Input);
            _uiRoot.Construct(_progressionController.Model, _playerModel, _wallet, _gameManager, _playerModel, _upgradeController.Model, _upgradeController, _progressionController);

            _tickables.Add(_enemySpawnerController);
            _tickables.Add(_playerModel);
        }

        private void Initialization()
        {
            _playerView.OnTouchedCoin += AddCoin;
            _upgradeController.Init();
            UIManager.ShowScreen<MenuScreen>();
        }

        private void AddCoin(Coin coin)
        {
            coin.PickUp();
            _wallet.AddCoin();
        }
    }
}