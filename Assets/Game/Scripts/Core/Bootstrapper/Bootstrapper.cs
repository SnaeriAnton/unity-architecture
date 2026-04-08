using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.LoaderSystem;
using Core.UI;

namespace Core.BootstrapperSystem
{
    public static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            BootManager.Reset();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RunBeforeFirstScene() => Run().Forget();

        private static async UniTask Run()
        {
            if (SceneManager.GetActiveScene().name != ScenesName.Boot.ToString())
                await Loader.LoadSceneAsync(ScenesName.Boot);

            BootManager.MarkReady();
            await UniTask.WaitUntil(() => BootManager.Initialized);
            await Loader.LoadSceneAsync(ScenesName.Game, () => Object.FindAnyObjectByType<GameComposer>(FindObjectsInactive.Include).StartGame());
        }
    }
}