using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _health;
        
        public void Show() => _health.enabled = true;
        public void Hide() => _health.enabled = false;
    }
}
