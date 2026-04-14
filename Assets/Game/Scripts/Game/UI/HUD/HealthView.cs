using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _health;
        
        public void Show() => _health.enabled = true;
        public void Hide() => _health.enabled = false;
        
        public class Pool : MonoMemoryPool<HealthView>
        {
            protected override void OnDespawned(HealthView item)
            {
                item.Hide();
                base.OnDespawned(item);
            }
        }
    }
}
