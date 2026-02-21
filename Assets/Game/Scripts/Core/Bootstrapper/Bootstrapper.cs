using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.LoaderSystem;
using Core.UI;
using Core.GSystem;

namespace Core.BootstrapperSystem
{
    public static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            BootManager.Reset();
            G.Reset();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RunBeforeFirstScene() => Run().Forget();

        private static async UniTask Run()
        {
            if (SceneManager.GetActiveScene().name != ScenesName.Boot.ToString())
                await Loader.LoadSceneAsync(ScenesName.Boot);

            G.Init(new Main());
            G.Main.Register<IUIService>(new UIService());
            BootManager.MarkReady();
            await UniTask.WaitUntil(() => G.Initialized);
            await UniTask.WaitUntil(() => BootManager.Initialized);
            await Loader.LoadSceneAsync(ScenesName.Game, () => Object.FindAnyObjectByType<EntryPoint>(FindObjectsInactive.Include).StartGame());
        }
    }
}