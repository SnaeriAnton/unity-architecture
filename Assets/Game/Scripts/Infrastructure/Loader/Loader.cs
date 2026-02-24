using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class Loader
    {
        public static async UniTask LoadSceneAsync(ScenesName name, Action onCallback = null, float delay = 0f, CancellationToken ct = default)
        {
            if (SceneManager.GetActiveScene().name == name.ToString())
            {
                onCallback?.Invoke();
                return;
            }

            if (delay > 0f) await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: ct);

            AsyncOperation op = SceneManager.LoadSceneAsync(name.ToString());

            await op.ToUniTask(cancellationToken: ct);
            await UniTask.WaitUntil(() => op.isDone, cancellationToken: ct);
            
            onCallback?.Invoke();
        }
    }
}