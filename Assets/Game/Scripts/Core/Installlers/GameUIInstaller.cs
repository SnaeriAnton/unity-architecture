using Core.UI;
using Game;
using UnityEngine;
using Zenject;

namespace Core.Installer
{
    public class GameUIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UIService>().AsSingle();
            Container.BindInterfacesAndSelfTo<HUDPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuScreenPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoseScreenPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeWindowPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIRegistry>().AsSingle();
        }
    }
}