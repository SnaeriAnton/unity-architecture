using System.Threading;
using Core.LoaderSystem;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Core.BootstrapperSystem
{
    public class AppStartup : IAsyncStartable
    {
        public async UniTask StartAsync(CancellationToken cancellation) => await Loader.LoadSceneAsync(ScenesName.Game);
    }
}