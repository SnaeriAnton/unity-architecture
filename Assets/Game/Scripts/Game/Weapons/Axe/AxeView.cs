using System;
using UnityEngine;
using Contracts;
using Core.Pool;

namespace Game
{
    public class AxeView : MonoBehaviour, IPoolable
    {
        private const float ANGULAR_SPEED_DEG = 90;
        
        public int PoolID { get; private set; }

        public event Action<IEnemyTarget> OnHit;
        private AxeModel _model;
        private Action _onDespawned;

        public void Init(AxeModel model)
        {
            _model = model;
            _model.OnStateChanged += Move;
            _model.OnDisable += Disable;
        }

        public void Disable()
        {
            _model.OnStateChanged -= Move;
            _model.OnDisable -= Disable;
            _onDespawned.Invoke();
        }

        private void Move()
        {
            transform.Rotate(0f, 0f, ANGULAR_SPEED_DEG * _model.Stats.RotationSpeed * Time.deltaTime, Space.Self);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _model.Direction, _model.Stats.FlightSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEnemyTarget target))
                OnHit?.Invoke(target);
        }
        
        void IPoolable.OnDespawned() => gameObject.SetActive(false);
        
        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            _onDespawned = onDespawned;
            gameObject.SetActive(true);
        }
    }
}