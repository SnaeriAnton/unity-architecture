using System;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Axe : MonoBehaviour, IPoolable
    {
        [SerializeField] private AxeEcsLink _axeLink;
        
        private GameApi _gameApi;
        private Action _onDespawned;

        public int PoolID { get; private set; }

        public void Init(GameApi gameApi, AxeStats stats, Vector3 direction)
        {
            _gameApi = gameApi;
            
            Entity entity = _gameApi.RegisterAxe(
                this,
                transform.position,
                direction,
                transform.rotation,
                stats.FlightSpeed,
                stats.LifeTime,
                stats.Damage,
                stats.RotationSpeed
                );
            
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

            _gameApi.RequestAxeHitPlayer(link.Entity);
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