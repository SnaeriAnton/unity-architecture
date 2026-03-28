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
        [SerializeField] private TypeOfCurrency _typeOfCurrency;

        private PlayerController _playerController;
        private Wallet _wallet;
        private PoolManager _poolManager;
        private EnemySpawnerController _enemySpawnerController;
        private ProgressionController _progressionController;
        private UpgradeController _upgradeController;
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
        private LoseScreenViewModel _loseScreenViewModel;
        private MenuScreenViewModel _menuScreenViewModel;
        private UpgradeWindowViewModel _upgradeWindowViewModel;
        private HUDViewModel _hudViewModel;
        private GameFlowCoordinator _gameFlowCoordinator;
        private PlayerViewModel _playerViewModel;

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
            _progressionController.Dispose();
            _playerController.Dispose();
            _playerView.Unbind();
            _menuScreenViewModel.Dispose();
            _loseScreenViewModel.Dispose();
            _hudViewModel.Dispose();
            _upgradeWindowViewModel.Dispose();
            _playerViewModel.Dispose();
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
            _playerTarget.Construct(_playerModel, _playerDamageHandler);
            _playerPickupHandler = new(_wallet);
            _playerViewModel = new(_playerModel);
            _playerView.Bind(_playerViewModel);
            _playerController = new(_playerViewModel, _playerView, _movement, _inputController, _playerModel, _playerPickupHandler, _playerDamageHandler, _weapon);
        }

        private void ComposeEnemy()
        {
            _enemyProcessor = new(_poolManager, _enemyDropConfig, _weapon, _gameLoop);
            _factory = new(_poolManager, _playerView.transform, _playerTarget, _enemyProcessor, _gameLoop);
            _enemySpawnerController = new(_generatorData, _factory, _border, _playerTarget, _gameLoop);
        }

        private void ComposeUpgrade()
        {
            _upgradeModel = new(_weaponLevelUpsData, _playerLevelUpsData);
            _upgradeController = new(_upgradeModel, _factory, _playerController, _gameConfig, _wallet, _weapon);
        }

        private void ComposeProgression()
        {
            _progressionModel = new(_config);
            _progressionController = new(_progressionModel, _playerPickupHandler, _upgradeModel, _enemySpawnerController, _wallet, _gameTime, _uiService);
        }

        private void ComposeGameFlow()
        {
            _gameFlowCoordinator = new(_uiService);
            _gameManager = new(_playerController, _enemySpawnerController, _progressionController, _upgradeController, _wallet, _poolManager, _gameFlowCoordinator, _inputRoot.Input);
        }

        private void ComposeUI()
        {
            _menuScreenViewModel = new(_gameManager.StartGame);
            _loseScreenViewModel = new(_gameManager.RestartRun);
            _hudViewModel = new(_progressionModel, _wallet, _playerModel, _upgradeController, _shieldModel);
            _upgradeWindowViewModel = new(_typeOfCurrency, _progressionController, _upgradeController, _upgradeModel, _wallet, () => _uiService.GetScreen<UpgradeWindowView>().Hide());
            _uiRoot.Construct(_loseScreenViewModel, _menuScreenViewModel, _upgradeWindowViewModel, _hudViewModel, _uiService);
        }

        private void RegisterTickables()
        {
            _gameLoop.Add(_enemySpawnerController);
            _gameLoop.Add(_playerController);
            _gameLoop.Add(_playerDamageHandler);
        }

        private void Initialization()
        {
            _upgradeController.Initialize();
            _playerController.Initialize();
            _uiService.ShowScreen<MenuScreenView>();
        }
    }
}