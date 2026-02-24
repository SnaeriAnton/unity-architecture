using System.Collections.Generic;
using UnityEngine;
using Application;
using Domain;
using Infrastructure;
using Runtime;

namespace Presentation
{
    public class GameComposer : MonoBehaviour
    {
        private readonly List<ITickable> _tickables = new();

        [SerializeField] private List<WeaponLevelUpsData> _weaponLevelUpsData;
        [SerializeField] private PlayerLevelUpsData _playerLevelUpsData;
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private Player _player;
        [SerializeField] private Border _board;
        [SerializeField] private GeneratorData _generatorData;
        [SerializeField] private ProgressConfig _config;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private InputRoot _inputRoot;
        [SerializeField] private PlayerStats _stats;

        private EnemySpawnerController _enemySpawnerController;
        private ProgressionService _progression;
        private UpgradeSystem _upgradeSystem;
        private GameSessionService _game;
        private Wallet _wallet;
        private Factory _factory;
        private PoolManager _poolManager;
        private EnemyDeathHandler _enemyHandler;
        private GameTime _gameTime;
        private WalletService _walletService;
        private PlayerSessionAdapter _playerSession;

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
            _playerSession = new(_player);
            List<GeneratorInfoStage> stages = new();
            _generatorData.Stages.ForEach(s=>stages.Add(new(s.Enemies, s.SpawnInterval)));
            GeneratorSettings generatorSettings = new(stages, _generatorData.CoinTemplate, _generatorData.CrystalTemplate, _generatorData.CoinsChanceOnSpawn, _generatorData.CrystalsChanceOnSpawn, _generatorData.RadiusPlayer);
            
            _gameTime = new();
            _inputRoot.Construct(IsMobile);
            _poolManager = new();
            _wallet = new(_gameConfig.StartCoinValues, _gameConfig.StartCrystalValues);
            _walletService = new(_wallet, _uiRoot.HUD);
            
            Dictionary<Weapons, Weapon> weaponTemplates = new();
            _weaponLevelUpsData.ForEach(w => weaponTemplates[w.Name] = w.WeaponTemplate);

            _factory = new(weaponTemplates, _poolManager, _player);
            _enemyHandler = new(_player, _poolManager, generatorSettings, _uiRoot.HUD);
            
            _enemySpawnerController = new(_player, generatorSettings, _enemyHandler, _factory, _board);

            GameSettings gameSettings = new(_gameConfig.StartWeapons, _gameConfig.StartCoinValues, _gameConfig.StartCrystalValues);
            List<WeaponUpgradeDefinition> weaponLevelUpsData = new();
            Dictionary<Weapons, Sprite> upgradeIconDictionary = new();
            
            foreach (WeaponLevelUpsData levelUps in _weaponLevelUpsData)
            {
                List<UpgradeDescription<WeaponStats>> weaponLevelUps = new();
                levelUps.LevelUps.ForEach(l => weaponLevelUps.Add(new(l.Type, l.Price, l.Stats)));
                weaponLevelUpsData.Add(new(weaponLevelUps, levelUps.Name));
                upgradeIconDictionary[levelUps.Name] = levelUps.Icon;
            }

            List<UpgradeDescription<SpartanStats>> playerLevelUps = new();
            _playerLevelUpsData.LevelUps.ForEach(l => playerLevelUps.Add(new(l.Type, l.Price, l.Stats)));
            upgradeIconDictionary[_playerLevelUpsData.Name] = _playerLevelUpsData.Icon;

            PlayerUpgradeDefinition playerLevelUpsData = new(playerLevelUps, _playerLevelUpsData.Name);
            _upgradeSystem = new(weaponLevelUpsData, playerLevelUpsData, gameSettings, _wallet, _uiRoot.HUD, _factory, _playerSession);

            ProgressSettings progressSettings = new(_config.ExperienceMultiplier, _config.LevelUpStageStep, _config.ExperienceBeforeLevelUp);
            _progression = new(_wallet, progressSettings, _gameTime, _uiRoot, _uiRoot.HUD, _enemySpawnerController, _upgradeSystem);
            _game = new(_wallet, _inputRoot.Input, _uiRoot, _playerSession, _enemySpawnerController, _progression, _poolManager, _upgradeSystem);
            _player.Construct(_walletService.AddCoin, _progression.PickUpCrystal, _game.GameOver, _uiRoot.HUD.Refresh, _board, _inputRoot.RuntimeInput, _stats.IFramesDuration, _stats.Speed);
            _uiRoot.Construct(upgradeIconDictionary, _progression, _upgradeSystem, _wallet, _game, _player);

            _tickables.Add(_enemySpawnerController);
        }

        private void Initialization()
        {
            _upgradeSystem.Init();
            _uiRoot.ShowMenu();
        }
    }
}