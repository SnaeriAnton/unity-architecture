using System.Collections;
using UnityEngine;

namespace Core.BootstrapperSystem
{
    public class BootManager : MonoBehaviour
    {
        private static bool _ready;
        
        [SerializeField] private GameObject _dontDestroyOnLoadObject;
        
        public static bool Initialized { get; private set; }

        public static void MarkReady() => _ready = true;

        public static void Reset()
        {
            Initialized = false;
            _ready = false;
        }
        
        private IEnumerator Start()
        {
            if (Initialized)
            {
                Debug.LogWarning("BootManager already initialized. Skipping.");
                yield break;
            }
            
            yield return new WaitUntil(() => _ready);
            DontDestroyOnLoad(_dontDestroyOnLoadObject);
            Initialized = true;
        }
    }
}