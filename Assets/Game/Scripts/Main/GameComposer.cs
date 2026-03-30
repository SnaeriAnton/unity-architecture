using System.Collections.Generic;
using UnityEngine;
using Application;
using Domain;
using Infrastructure;
using Presentation;

namespace Main
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
        [SerializeField] private PlayerStats _stats;

        private EnemySpawnerController _enemySpawnerController;
        private ProgressionService _progression;
        private UpgradeSystem _upgradeSystem;

        private bool IsMobile => UnityEngine.Application.isMobilePlatform;

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
            GameSettings gameSettings = new(_gameConfig.StartWeapons);
            ProgressSettings progressSettings = new(_config.ExperienceMultiplier, _config.LevelUpStageStep, _config.ExperienceBeforeLevelUp);
            GeneratorSettingsBuilder generatorSettingsBuilder = new(_generatorData);
            UpgradeDefinitionsBuilder upgradeDefinitionsBuilder = new(_weaponLevelUpsData, _playerLevelUpsData);
            UpgradeIconDictionaryBuilder upgradeIconDictionaryBuilder = new(_weaponLevelUpsData, _playerLevelUpsData);
            Wallet wallet = new(_gameConfig.StartCoinValues, _gameConfig.StartCrystalValues);
            GameTime gameTime = new();
            PoolManager poolManager = new();
            WalletAdapter walletAdapter = new(wallet);
            EnemyDeathHandler enemyHandler = new(_player, poolManager, generatorSettingsBuilder.Settings);
            ProjectileSpawner projectileSpawner = new(poolManager);
            WeaponFactory weaponFactory = new(upgradeDefinitionsBuilder.WeaponTemplates, _player, projectileSpawner);
            EnemyFactory enemyFactory = new(poolManager, _player, projectileSpawner);
            PlayerSessionAdapter playerSession = new(_player);
            
            _inputRoot.Construct(IsMobile);
            _enemySpawnerController = new(_player, generatorSettingsBuilder.Settings, enemyHandler, enemyFactory, _border);
            _upgradeSystem = new(upgradeDefinitionsBuilder.WeaponData, upgradeDefinitionsBuilder.PlayerData, gameSettings, walletAdapter, weaponFactory, playerSession);
            _progression = new(walletAdapter, progressSettings, gameTime, _uiRoot, _enemySpawnerController, _upgradeSystem);
            GameSessionService game = new(walletAdapter, _inputRoot.Input, _uiRoot, playerSession, _enemySpawnerController, _progression, poolManager, _upgradeSystem);
            _player.Construct(walletAdapter.AddCoin, _progression.PickUpCrystal, game.GameOver, _border, _inputRoot.Input, _stats.IFramesDuration, _stats.Speed);
            _uiRoot.Construct(upgradeIconDictionaryBuilder.IconDictionary, _progression, _progression, _upgradeSystem, _upgradeSystem, walletAdapter, game, _player, _player, enemyHandler);

            _tickables.Add(_enemySpawnerController);
        }

        private void Initialization()
        {
            _upgradeSystem.Init();
            _uiRoot.ShowMenu();
        }
    }
}