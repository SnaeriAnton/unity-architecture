using System.Collections.Generic;
using UnityEngine;
using Contracts;
using Core.InputSystem;
using Core.Pool;
using Core.UI;
using Game;

namespace Core
{
    public class GameComposer : MonoBehaviour
    {
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
        [SerializeField] private EnemyDropConfig _enemyDropConfig;
        [SerializeField] private PlayerTarget _playerTarget;

        private PlayerPresenter _playerPresenter;
        private Wallet _wallet;
        private PoolManager _poolManager;
        private EnemySpawnerPresenter _enemySpawnerPresenter;
        private ProgressionPresenter _progressionPresenter;
        private UpgradePresenter _upgradePresenter;
        private GameManager _gameManager;
        private Factory _factory;
        private EnemyDeathProcessor _enemyProcessor;
        private WeaponSystem _weapon;
        private PlayerInputController _inputController;
        private PlayerMovement _movement;
        private PlayerModel _playerModel;
        private PlayerPickupHandler _playerPickupHandler;
        private PlayerDamageHandler _playerDamageHandler;
        private UIService _uiService;
        private ProgressionModel _progressionModel;
        private GameTime _gameTime;
        private UpgradeModel _upgradeModel;
        private GameLoop _gameLoop;
        private ShieldModel _shieldModel;

        private bool IsMobile => Application.isMobilePlatform;

        public void StartGame()
        {
            Compose();
            Initialization();
        }

        private void Update() => _gameLoop.Tick(Time.deltaTime);
        private void OnDestroy() => Dispose();

        private void Dispose()
        {
            _uiRoot.Dispose();
            _uiService.Dispose();
            _gameManager.Dispose();
            _progressionPresenter.Dispose();
            _playerPresenter.Dispose();
        }

        private void Compose()
        {
            ComposeCore();
            ComposePlayer();
            ComposeEnemy();
            ComposeUpgrade();
            ComposeProgression();
            ComposeGameFlow();
            ComposeUI();
            RegisterTickables();
        }

        private void ComposeCore()
        {
            _inputRoot.Construct(IsMobile);
            _uiService = new();
            _shieldModel = new();
            _weapon = new(_shieldModel);
            _gameTime = new();
            _gameLoop = new();
            _wallet = new(_gameConfig.StartCoinValues, _gameConfig.StartCrystalValues);
        }

        private void ComposePlayer()
        {
            _movement = new(_border, _playerStats.Speed);
            _inputController = new(_movement, _inputRoot.Input);
            _playerModel = new();
            _poolManager = new();
            _playerDamageHandler = new(_playerModel, _weapon, _playerStats.IFramesDuration);
            _playerTarget.Construct(_playerModel, _playerView, _playerDamageHandler);
            _playerPickupHandler = new(_wallet);
            _playerPresenter = new(_playerView, _movement, _inputController, _playerModel, _playerPickupHandler, _playerDamageHandler, _weapon);
        }

        private void ComposeEnemy()
        {
            _enemyProcessor = new(_poolManager, _enemyDropConfig, _weapon, _gameLoop);
            _factory = new(_poolManager, _playerView.transform, _playerTarget, _enemyProcessor, _gameLoop);
            _enemySpawnerPresenter = new(_generatorData, _factory, _border, _playerTarget, _gameLoop);
        }

        private void ComposeUpgrade()
        {
            _upgradeModel = new(_weaponLevelUpsData, _playerLevelUpsData);
            _upgradePresenter = new(_upgradeModel, _factory, _playerPresenter, _gameConfig, _wallet, _weapon);
        }

        private void ComposeProgression()
        {
            _progressionModel = new(_config);
            _progressionPresenter = new(_progressionModel, _playerPickupHandler, _upgradeModel, _enemySpawnerPresenter, _wallet, _gameTime);
        }

        private void ComposeGameFlow() => _gameManager = new(_playerPresenter, _enemySpawnerPresenter, _progressionPresenter, _upgradePresenter, _wallet, _poolManager, _inputRoot.Input, _uiService);
        private void ComposeUI() => _uiRoot.Construct(_progressionModel, _wallet, _gameManager, _playerModel, _shieldModel, _upgradeModel, _upgradePresenter, _progressionPresenter, _uiService);

        private void RegisterTickables()
        {
            _gameLoop.Add(_enemySpawnerPresenter);
            _gameLoop.Add(_playerPresenter);
            _gameLoop.Add(_playerDamageHandler);
        }

        private void Initialization()
        {
            _upgradePresenter.Initialize();
            _uiRoot.Initialize();
            _playerPresenter.Initialize();
            _uiService.ShowScreen<MenuScreenView>();
        }
    }
}