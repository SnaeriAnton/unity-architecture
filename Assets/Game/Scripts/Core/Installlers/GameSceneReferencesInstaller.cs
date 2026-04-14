using UnityEngine;
using Zenject;
using Game;

namespace Core.Installer
{
    public class GameSceneReferencesInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private PlayerTarget _playerTarget;
        [SerializeField] private Border _border;
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private TypeOfCurrency _typeOfCurrency;
        
        public override void InstallBindings()
        {

            Container.BindInstance(_playerView).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerTarget>().FromInstance(_playerTarget).AsSingle();
            Container.BindInterfacesAndSelfTo<Border>().FromInstance(_border).AsSingle();
            Container.BindInstance(_uiRoot.HudView).AsSingle();
            Container.BindInstance(_uiRoot.MenuScreenView).AsSingle();
            Container.BindInstance(_uiRoot.LoseScreenView).AsSingle();
            Container.BindInstance(_uiRoot.UpgradeWindowView).AsSingle();
            Container.BindInstance(_typeOfCurrency).AsSingle();
            Container.Bind<Transform>().WithId("WeaponRoot").FromResolveGetter<PlayerView>(x => x.WeaponRoot).AsSingle();
        }
    }
}