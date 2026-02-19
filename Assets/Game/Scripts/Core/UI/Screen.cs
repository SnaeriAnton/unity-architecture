using UnityEngine;

namespace Core.UI
{
    public abstract class Screen : MonoBehaviour
    {
        public virtual void Dispose() { }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();
        }

        public virtual void Hide() => gameObject.SetActive(false);
        public virtual void Reset() { }

        //[Button]
        private void ShowScreen() => Show();
            
        //[Button]
        private void HideScreen() =>  Hide();
    }
}