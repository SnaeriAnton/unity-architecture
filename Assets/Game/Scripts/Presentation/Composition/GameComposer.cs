using System.Collections.Generic;
using UnityEngine;
using Application;
using Infrastructure;

namespace Presentation
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
        [SerializeField] private TypeOfCurrency _typeOfCurrency;
        
        private Factory _factory;
        private PoolManager _poolManager;
        private GameTime _gameTime;
        private GameFactory _gameFactory;

        private bool IsMobile => UnityEngine.Application.isMobilePlatform;

        public void StartGame()
        {
            Compose();
            Initialization();
        }

        private void Update() => _tickables.ForEach(t => t.Tick());
        private void OnDestroy() => Dispose();
        private void Dispose() => _uiRoot.Dispose(); 

        private void Compose()
        {
            _gameTime = new();
            _inputRoot.Construct(IsMobile);
            _poolManager = new();
            _factory = new(_poolManager, _player);
            ProgressSettings progressSettings = new(_config.ExperienceMultiplier, _config.LevelUpStageStep, _config.ExperienceBeforeLevelUp);
            _gameFactory = new(_weaponLevelUpsData, _playerLevelUpsData, _factory, progressSettings, _player, _gameConfig, _gameTime, _border, _poolManager, _generatorData, _inputRoot.Input, _gameConfig.StartCoinValues, _gameConfig.StartCrystalValues);
            _player.Construct(_gameFactory.WalletService.AddCoin, _gameFactory.Progression.PickUpCrystal, _gameFactory.Game.GameOver, _uiRoot.HUD.Refresh, _border, _inputRoot.Input, _stats.IFramesDuration, _stats.Speed);
            _uiRoot.Construct(_typeOfCurrency.GetSprites(), _gameFactory.Progression, _gameFactory.UpgradeSystem, _gameFactory.WalletService, _gameFactory.Game, _player, _gameFactory.EnemyHandler);

            _tickables.Add(_gameFactory.EnemySpawnerController);
        }

        private void Initialization()
        {
            _gameFactory.Init();
            _uiRoot.ShowMenu();
        }
    }
}