using System;
using Contracts;
using UnityEngine;
using Core.Pool;
using UniRx;

namespace Game
{
    public class Axe : MonoBehaviour, IPoolable, IDisposable
    {
        private const float ANGULAR_SPEED_DEG = 90;

        private CompositeDisposable _disposable = new();
        private Action _onDespawned;
        private AxeStats _stats;
        private Vector3 _direction;
        private IUpdateStream _updateStream;

        public int PoolID { get; private set; }

        public void Init(IUpdateStream updateStream, AxeStats stats, Vector3 direction)
        {
            _updateStream = updateStream;
            _stats = stats;
            _direction = direction;
            _disposable = new();

            _updateStream.OnUpdate.Subscribe(Tick).AddTo(_disposable);
        }

        private void Tick(float dt)
        {
            transform.Rotate(0f, 0f, ANGULAR_SPEED_DEG * _stats.RotationSpeed * Time.deltaTime, Space.Self);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, _stats.FlightSpeed * dt);

            _stats.LifeTime -= dt;
            if (_stats.LifeTime <= 0) Destroy();
        }

        public void Dispose() => _disposable?.Dispose();

        void IPoolable.OnDespawned()
        {
            Dispose();
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ITarget target))
            {
                target.TakeDamage(_stats.Damage);
                Destroy();
            }
        }

        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            _onDespawned = onDespawned;
            gameObject.SetActive(true);
        }

        private void Destroy() => _onDespawned?.Invoke();
    }
}