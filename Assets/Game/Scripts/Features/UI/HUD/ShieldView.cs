using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShieldView : MonoBehaviour
    {
        [SerializeField] private Image _shieldCoolDownImage;

        public void UpdateCoolDown(float coolDown, float maxCoolDown)
        {
            gameObject.SetActive(true);
            _shieldCoolDownImage.fillAmount = coolDown / maxCoolDown;
        }
        
        public void Deactivate() => gameObject.SetActive(false);
    }
}