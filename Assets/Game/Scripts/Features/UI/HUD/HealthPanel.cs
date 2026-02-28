using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class HealthPanel : MonoBehaviour
    {
        [SerializeField] private HealthView _healthViewTemplate;

        private readonly Queue<HealthView> _healthQueue = new();
        private readonly List<HealthView> _healthList = new();

        public void UpdateHealth(int health)
        {
            for (int i = _healthList.Count; i < health; i++)
                GetHealth();

            _healthList.ForEach(h => h.Show());
        }

        public void Reset()
        {
            foreach (HealthView health in _healthList)
            {
                health.gameObject.SetActive(false);
                _healthQueue.Enqueue(health);
            }
            
            _healthList.Clear();
        }

        public void ChangeHealth(int currentHealth)
        {
            _healthList.ForEach(h => h.Hide());
            for (int i = 0; i < currentHealth; i++)
                _healthList[i].Show();
        }

        private HealthView GetHealth()
        {
            if (!_healthQueue.TryPeek(out HealthView view))
                view = Instantiate(_healthViewTemplate, transform);
            
            view.gameObject.SetActive(true);
            _healthList.Add(view);

            return view;
        }
    }
}