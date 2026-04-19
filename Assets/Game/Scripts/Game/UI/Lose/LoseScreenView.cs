using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LoseScreenView : Core.UI.Screen
    {
        [SerializeField] private Button _homeButton;

        public event Action OnClick;
        
        public void Initialize() => _homeButton.onClick.AddListener(Click);
        public void Dispose() => _homeButton.onClick.RemoveListener(Click);
        private void Click() => OnClick?.Invoke();
    }
}