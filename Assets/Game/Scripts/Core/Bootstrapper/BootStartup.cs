using Core.LoaderSystem;
using Zenject;

namespace Core.BootstrapperSystem
{
    public class BootStartup : IInitializable
    {
        private readonly ZenjectSceneLoader _sceneLoader;
        public BootStartup(ZenjectSceneLoader sceneLoader) => _sceneLoader = sceneLoader;

        public void Initialize() => _sceneLoader.LoadScene(ScenesName.Game.ToString());
    }
}