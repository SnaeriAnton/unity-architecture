using System.Collections.Generic;
using Contracts;
using Core.InputSystem;
using Core.Pool;
using Core.UI;
using Game;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private List<WeaponLevelUpsData> _weaponLevelUpsData;
        [SerializeField] private PlayerLevelUpsData _playerLevelUpsData;
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private Border _border;
        [SerializeField] private PlayerTarget _playerTarget;
        [SerializeField] private GeneratorData _generatorData;
        [SerializeField] private ProgressConfig _config;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private EnemyDropConfig _enemyDropConfig;
        [SerializeField] private TypeOfCurrency _typeOfCurrency;

        protected override void Configure(IContainerBuilder builder)
        {
            InfrastructureConfigure(builder);
            GameConfigsConfigure(builder);
            GameDomainConfigure(builder);
            GameFactoriesConfigure(builder);
            GamePlayerConfigure(builder);
            GameSceneReferencesConfigure(builder);
            GameUIConfigure(builder);
            InputConfigure(builder);
        }

        private void InfrastructureConfigure(IContainerBuilder builder)
        {
            builder.Register<PoolManager>(Lifetime.Scoped).AsSelf().As<IPoolService>();
            builder.RegisterEntryPoint<GameStartup>();
            builder.RegisterEntryPoint<RuntimeTickRegistry>().As<IRuntimeTickRegistry>();
        }
        
        private void GameConfigsConfigure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerLevelUpsData);
            builder.RegisterInstance(_playerStats);
            builder.RegisterInstance(_generatorData);
            builder.RegisterInstance(_config);
            builder.RegisterInstance(_gameConfig);
            builder.RegisterInstance(_enemyDropConfig);
        }
        
        private void GameDomainConfigure(IContainerBuilder builder)
        {
            builder.Register<ShieldModel>(Lifetime.Scoped).AsSelf().As<IShieldReadModel>();
            builder.Register<WeaponSystem>(Lifetime.Scoped).AsSelf().As<IWeaponSystem>();
            builder.Register(_ => new Wallet(_gameConfig.StartCoinValues, _gameConfig.StartCrystalValues), Lifetime.Scoped).AsSelf().As<IWallet>().As<IWalletWriter>().As<ISpentable>();
            builder.Register<GameTime>(Lifetime.Scoped).AsSelf().As<IGamePauseService>();
            builder.Register<PlayerMovement>(Lifetime.Scoped).AsSelf();
            builder.Register<PlayerModel>(Lifetime.Scoped).AsSelf().As<IPlayerReadModel>();
            builder.Register<PlayerInputController>(Lifetime.Scoped).AsSelf();
            builder.RegisterEntryPoint<PlayerDamageHandler>().AsSelf();
            builder.Register<PlayerPickupHandler>(Lifetime.Scoped).AsSelf().As<IPickupReceiver>();
            builder.Register<ProgressionModel>(Lifetime.Scoped).AsSelf().As<IProgressionReadModel>();
            builder.Register<UpgradeModel>(Lifetime.Scoped).WithParameter<IReadOnlyList<WeaponLevelUpsData>>(_weaponLevelUpsData).AsSelf().AsImplementedInterfaces();
            builder.RegisterEntryPoint<ProgressionPresenter>(Lifetime.Scoped).AsSelf().As<IProgression>();
            builder.RegisterEntryPoint<UpgradePresenter>(Lifetime.Scoped).AsSelf().As<IUpgrade>();
            builder.RegisterEntryPoint<GameManager>(Lifetime.Scoped).AsSelf();
            builder.Register<EnemyDeathProcessor>(Lifetime.Scoped).AsSelf().As<IEnemyDeathProcessor>();
            builder.RegisterEntryPoint<EnemySpawnerPresenter>().AsSelf().As<IEnemyStageProgression>();
        }
        
        private void GameFactoriesConfigure(IContainerBuilder builder)
        {
            builder.Register<EnemyFactory>(Lifetime.Scoped).AsSelf().As<IEnemyFactory>();
            builder.Register<ProjectileFactory>(Lifetime.Scoped).AsSelf().As<IProjectileFactory>();
            builder.Register<WeaponFactory>(Lifetime.Scoped).AsSelf().As<IWeaponFactory>();
        }
        
        private void GamePlayerConfigure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PlayerPresenter>().AsSelf().As<IPlayer>();
        }
        
        private void GameSceneReferencesConfigure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerView);
            builder.RegisterComponent(_playerTarget).AsSelf().As<ITarget>();
            builder.RegisterComponent(_border).AsSelf().As<ISpawnPointProvider>();
            builder.RegisterComponent(_uiRoot.HudView);
            builder.RegisterComponent(_uiRoot.LoseScreenView);
            builder.RegisterComponent(_uiRoot.UpgradeWindowView);
            builder.RegisterComponent(_uiRoot.MenuScreenView);
            builder.RegisterInstance(_typeOfCurrency);
            builder.RegisterInstance(_playerView.WeaponRoot).Keyed("WeaponRoot");
        }

        private void GameUIConfigure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<UIService>(Lifetime.Scoped).AsSelf().As<IUIService>();
            builder.RegisterEntryPoint<HUDPresenter>().AsSelf();
            builder.RegisterEntryPoint<MenuScreenPresenter>().AsSelf();
            builder.RegisterEntryPoint<LoseScreenPresenter>().AsSelf();
            builder.RegisterEntryPoint<UpgradeWindowPresenter>().AsSelf();
            builder.RegisterEntryPoint<UIRegistry>().AsSelf();
        }

        private void InputConfigure(IContainerBuilder builder)
        {
            if (Application.isMobilePlatform)
                builder.RegisterEntryPoint<Mobile>(Lifetime.Scoped).As<IInput>();
            else
                builder.RegisterEntryPoint<PC>(Lifetime.Scoped).As<IInput>();
        }
    }
}