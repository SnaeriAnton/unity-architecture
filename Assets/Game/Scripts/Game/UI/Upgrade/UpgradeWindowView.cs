using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core.UI;

namespace Game
{
    public class UpgradeWindowView : Window
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _crystalText;
        
        public event Action OnClick;
        
        [field: SerializeField] public RectTransform ButtonsContainer { get; private set; }
        [field: SerializeField] public UpgradeButtonView UpgradeButtonViewTemplate { get; private set; }
        
        public void Initialize() => _closeButton.onClick.AddListener(Click);
        public void Dispose() => _closeButton.onClick.RemoveListener(Click);
        private void Click() => OnClick?.Invoke();
        
        public void UpdateValues(int coins, int crystals)
        {
            _crystalText.text = crystals.ToString();
            _coinsText.text = coins.ToString();
        }
    }
}