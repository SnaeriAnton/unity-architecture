using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MenuScreenView : Core.UI.Screen
    {
        [SerializeField] private Button _startGameButton;

        public event Action OnClick;
        
        public void Initialize() => _startGameButton.onClick.AddListener(Click);
        public void Dispose() => _startGameButton.onClick.RemoveListener(Click);
        private void Click() => OnClick?.Invoke();
    }
}
