using System;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Axe : MonoBehaviour, IPoolable
    {
        [SerializeField] private AxeEcsLink _axeLink;
        
        private Action _onDespawned;
        private AxeApi _axeApi;

        public int PoolID { get; private set; }

        public void Init(AxeApi axeApi, AxeStats stats, Vector3 direction)
        {
            _axeApi = axeApi;
            
            int entity = _axeApi.RegisterAxe(
                this,
                transform,
                DespawnByEcs,
                transform.position,
                direction,
                transform.rotation,
                stats.FlightSpeed,
                stats.LifeTime,
                stats.Damage,
                stats.RotationSpeed);
            
            _axeLink.Bind(entity);
        }

        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<PlayerHitbox>(out _))
                return;

            AxeEcsLink link = GetComponent<AxeEcsLink>();

            if (!link.IsRegistered)
                return;

            _axeApi.RequestAxeHitPlayer(link.Entity);
        }
        
        public void DespawnByEcs()
        {
            gameObject.SetActive(false);
            _onDespawned.Invoke();
        }

        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            _onDespawned = onDespawned;
            gameObject.SetActive(true);
        }
    }
}