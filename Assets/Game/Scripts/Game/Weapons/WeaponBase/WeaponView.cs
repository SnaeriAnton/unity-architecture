using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class WeaponView : MonoBehaviour
    {
        protected WeaponModel _model;
        protected PoolManager _poolManager;

        public virtual void Construct(PoolManager poolManager) => _poolManager = poolManager;

        public virtual void Init(WeaponModel model)
        {
            _model = model;
            _model.OnStatsChanged += SetStats;
            _model.OnReset += Reset;
        }

        public virtual void Dispose()
        {
            _model.OnStatsChanged -= SetStats;
            _model.OnReset -= Reset;
        }

        protected virtual void SetStats() => gameObject.SetActive(true);
        protected virtual void Reset() => gameObject.SetActive(false);
    }
}