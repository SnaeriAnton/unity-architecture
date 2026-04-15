using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Game;

namespace Core.Installer
{
    public class GameConfigsInstaller : MonoInstaller
    {
        [SerializeField] private List<WeaponLevelUpsData> _weaponLevelUpsData;
        [SerializeField] private PlayerLevelUpsData _playerLevelUpsData;
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private GeneratorData _generatorData;
        [SerializeField] private ProgressConfig _config;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private EnemyDropConfig _enemyDropConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<IReadOnlyList<WeaponLevelUpsData>>().FromInstance(_weaponLevelUpsData).AsSingle();
            Container.BindInstance(_playerLevelUpsData).AsSingle();
            Container.BindInstance(_playerStats).AsSingle();
            Container.BindInstance(_generatorData).AsSingle();
            Container.BindInstance(_config).AsSingle();
            Container.BindInstance(_gameConfig).AsSingle();
            Container.BindInstance(_enemyDropConfig).AsSingle();
            Container.BindInstance(new WalletSettings(_gameConfig.StartCoinValues, _gameConfig.StartCrystalValues)).AsSingle();
        }
    }
}