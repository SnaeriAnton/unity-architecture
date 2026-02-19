using UnityEngine;
using TMPro;

namespace Game
{
    public class CoinsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsText;
        
        public void ShowCoinsText(int coins) => _coinsText.text = coins.ToString(); 
    }
}
