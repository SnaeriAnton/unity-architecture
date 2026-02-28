using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _health;
        
        public void Show() => _health.enabled = true;
        public void Hide() => _health.enabled = false;
    }
}
