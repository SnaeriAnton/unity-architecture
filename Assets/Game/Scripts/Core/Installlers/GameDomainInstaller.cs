using Zenject;
using Contracts;
using Game;

namespace Core.Installer
{
    public class GameDomainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ShieldModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<Wallet>().AsSingle();
            Container.BindInterfacesTo<GameTime>().AsSingle();
            Container.Bind<PlayerMovement>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle();
            Container.Bind<PlayerInputController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDamageHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPickupHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProgressionModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProgressionPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradePresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            Container.Bind<IEnemyDeathProcessor>().To<EnemyDeathProcessor>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawnerPresenter>().AsSingle();
        }
    }
}