using UnityEngine;

namespace Game
{
    public class PlayerDeathView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _eyesSpriteRenderer;

        public void ShowDeath() => _eyesSpriteRenderer.enabled = false;

        public void ResetDeathView() => _eyesSpriteRenderer.enabled = true;
    }
}