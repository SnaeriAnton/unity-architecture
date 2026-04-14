using Zenject;

namespace Core.BootstrapperSystem
{
    public class BootSceneInstaller : MonoInstaller
    {
        public override void InstallBindings() => Container.BindInterfacesTo<BootStartup>().AsSingle().NonLazy();
    }
}