using Contracts;
using Game;
using Zenject;

namespace Core.Installer
{
    public class GameFactoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
            Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle();
            Container.Bind<IProjectileFactory>().To<ProjectileFactory>().AsSingle();
        }
    }
}