using Game;
using Zenject;

namespace Core.Installer
{
    public class GamePlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerPresenter>().AsSingle();
        }
    }
}