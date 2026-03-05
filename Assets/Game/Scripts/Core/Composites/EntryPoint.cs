using System.Collections.Generic;
using UnityEngine;
using Core.GSystem;
using Contracts;
using Core.InputSystem;
using Core.Pool;
using Core.UI;
using Game;

namespace Core
{
    public class EntryPoint : MonoBehaviour
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

        private Main _g = G.Main;
        private EnemySpawnerController _enemySpawnerController;
        private ProgressionSystem _progressionSystem;
        private UpgradeSystem _upgradeSystem;
        private GameManager _gameManager;
        private Wallet _wallet;
        private Factory _factory;
        private PoolManager _poolManager;
        private EnemyDeathHandler _enemyHandler;
        
        public void StartGame()
        {
            RegisterSceneObjectsToG();
            Compose();
            RegisterRuntimeServicesToG();
            Initialization();
            
        }

        private void Update() => _tickables.ForEach(t => t.Tick());
        private void OnDestroy() => Dispose();
        private void Dispose() { }

        private void Compose()
        {
            _poolManager = new();
            _wallet = new(_gameConfig.StartCoinValues, _gameConfig.StartCrystalValues);
            _factory = new();
            _enemyHandler = new(_player, _poolManager, _generatorData);
            _enemySpawnerController = new(_player, _generatorData, _enemyHandler, _factory, _border);
            _upgradeSystem = new(_weaponLevelUpsData, _playerLevelUpsData, _factory, _player, _gameConfig, _wallet);
            
            _progressionSystem = new(_upgradeSystem, _enemySpawnerController, _wallet, _config);
            _gameManager = new(_enemySpawnerController, _progressionSystem, _upgradeSystem);
            _player.Construct(_progressionSystem.PickUpCrystal, _gameManager.GameOver);

            _tickables.Add(_enemySpawnerController);
        }

        private void RegisterSceneObjectsToG()
        {
            _g.Register(this);
            _g.Register(_uiRoot);
            _g.Register(_player);
            _g.Register(_border);
            _g.Register(_generatorData);
            _g.Register(_config);
            _g.Register(_gameConfig);
            
            _g.Register(_weaponLevelUpsData);
            _g.Register(_playerLevelUpsData);
            
            _g.Register<IInput>(_inputRoot.Input);
        }

        private void RegisterRuntimeServicesToG()
        {
            _g.Register(_poolManager);
            _g.Register(_wallet);
            _g.Register(_factory);
            _g.Register(_enemyHandler);
            _g.Register(_enemySpawnerController);
            _g.Register(_upgradeSystem);
            _g.Register(_progressionSystem);
            _g.Register<IGameManager>(_gameManager);

        }
        

        private void Initialization()
        {
            _upgradeSystem.Init();
            G.Main.Resolve<IUIService>().ShowScreen<MenuScreen>();
        }
    }
}