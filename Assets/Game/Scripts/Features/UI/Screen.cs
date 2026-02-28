using UnityEngine;

namespace UI
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
    }
}