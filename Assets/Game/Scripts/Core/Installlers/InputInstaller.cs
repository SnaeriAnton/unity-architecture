using Zenject;
using Core.InputSystem;

namespace Core.Installer
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            if (UnityEngine.Application.isMobilePlatform)
                Container.BindInterfacesTo<Mobile>().AsSingle();
            else
                Container.BindInterfacesTo<PC>().AsSingle();
        }
    }
}