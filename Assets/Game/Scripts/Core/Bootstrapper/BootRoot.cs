using UnityEngine;
using Core.LoaderSystem;
using Core.UI;

namespace Core
{
    public class BootRoot : MonoBehaviour
    {
        [SerializeField] private GameObject _dontDestroyOnLoadObject;

        private void Start()
        {
            UIManager.Dispose();
            DontDestroyOnLoad(_dontDestroyOnLoadObject);
            Loader.LoadSceneAsync(ScenesName.Game);
        }
    }
}