using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class Weapon : MonoBehaviour
    {
        protected PoolManager _poolManager;
        protected WeaponStats _stats;
        protected GameApi _gameApi;

        public virtual void Construct(PoolManager poolManager, GameApi gameApi)
        {
            _gameApi = gameApi;
            _poolManager = poolManager;
        }

        public virtual void UpdateValues() { }

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