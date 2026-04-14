using UnityEngine;
using Zenject;

namespace Game
{
    public abstract class Weapon : MonoBehaviour
    {
        protected WeaponStats _stats;
        protected TickableManager _tickableManager;

        [Inject]
        public virtual void Construct(TickableManager tickableManager)
        {
            _tickableManager = tickableManager;
        }

        public virtual void Tick(float dt) { }
        public virtual void RefreshState() { }

        public virtual void SetStats(WeaponStats stats)
        {
            _stats = stats;
            gameObject.SetActive(true);
        }

        public virtual void Reset()
        {
            _stats = default;
            gameObject.SetActive(false);
        }
    }
}