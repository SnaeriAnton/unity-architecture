using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBarView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        
        public void UpdateProgressbar(int currentProgress, int maxUpgrade) => _slider.value = (float)currentProgress / maxUpgrade;
    }
}
