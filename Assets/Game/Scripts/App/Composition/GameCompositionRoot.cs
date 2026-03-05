using System;
using System.Collections.Generic;
using UnityEngine;
using Core.InputSystem;
using Core.Pool;
using Economy;
using Enemies;
using GameFlow;
using Loot;
using Progression;
using Shared;
using UI;
using Upgrades;
using Weapons;
using World;

namespace App
{
    public class GameCompositionRoot : MonoBehaviour
    {
        private readonly List<ITickable> _tickables = new();

        [SerializeField] private List<WeaponLevelUpsData> _weaponLevelUpsData;
        [SerializeField] private PlayerLevelUpsData _playerLevelUpsData;
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private Player.Player _player;
        [SerializeField] private Border _border;
        [SerializeField] private GeneratorData _generatorData;
        [SerializeField] private ProgressConfig _config;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private InputRoot _inputRoot;
        [SerializeField] private WeaponCatalog _weaponCatalog;
        [SerializeField] private LooCatalog _looCatalog;

        private EnemySpawnerController _enemySpawnerController;
        private ProgressionSystem _progressionSystem;
        private UpgradeSystem _upgradeSystem;
        private GameManager _gameManager;
        private Wallet _wallet;
        private WeaponsFactory _weaponsFactory;
        private PoolManager _poolManager;
        private EnemyDeathHandler _enemyHandler;
        private EnemiesFactory _enemiesFactory;
        private WeaponSystem _weaponSystem;
        private LootFactory _lootFactory;

        private bool IsMobile => Application.isMobilePlatform;

        private void Awake()
        {
            Compose();
            Initialization();
        }

        private void Update() => _tickables.ForEach(t => t.Tick());
        private void OnDestroy() => Dispose();

        private void Dispose()
        {
            _progressionSystem?.Dispose();
            _gameManager?.Dispose();
            _uiRoot.Dispose();

            GameEvents.Loot.CoinPicked -= _wallet.AddCoin;
        }

        private void Compose()
        {
            _inputRoot.Construct(IsMobile);
            _poolManager = new();
            _lootFactory = new(_looCatalog, _poolManager);
            _weaponSystem = new();
            _wallet = new(_gameConfig.StartCoinValues, _gameConfig.StartCrystalValues);
            _weaponsFactory = new(_weaponSystem, _weaponCatalog, _poolManager);
            _enemiesFactory = new(_poolManager, _player);
            _enemyHandler = new(_player, _poolManager, _lootFactory, _generatorData);
            _enemySpawnerController = new(_player, _generatorData, _enemyHandler, _enemiesFactory, _border);
            _upgradeSystem = new(_weaponLevelUpsData, _gameConfig.StartWeapons, _playerLevelUpsData, _weaponsFactory, _player, _wallet);

            _progressionSystem = new(_config, _upgradeSystem, _enemySpawnerController, _wallet);
            _gameManager = new(_player, _enemySpawnerController, _progressionSystem, _upgradeSystem, _wallet, _poolManager, _inputRoot.Input);
            _player.Construct(_gameManager.GameOver, _weaponSystem, _border, _inputRoot.Input);
            _uiRoot.Construct(_upgradeSystem);

            _tickables.Add(_enemySpawnerController);
        }

        private void Initialization()
        {
            _upgradeSystem.Init();
            GameEvents.Loot.CoinPicked += _wallet.AddCoin;
            _wallet.Init();
            GameEvents.GameFlow.RaiseScreenRequested(GameScreen.Menu);
        }
    }
}