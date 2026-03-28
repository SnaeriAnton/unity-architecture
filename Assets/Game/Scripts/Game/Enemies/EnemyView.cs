using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public abstract class EnemyView : MonoBehaviour, IPoolable
    {
        protected readonly List<IDisposable> _disposables = new();
        protected EnemyViewModel _viewModel;

        public Vector3 Position => transform.position;
        public int PoolID { get; private set; }

        public event Action<Collider2D> OnTrigger;

        public virtual void Bind(EnemyViewModel viewModel)
        {
            Unbind();
            _viewModel = viewModel;
            _disposables.Add(_viewModel.Position.Subscribe(SetPosition));
            _disposables.Add(_viewModel.IsDead.Subscribe(Die));
        }

        public virtual void Unbind()
        {
            _disposables.ForEach(d => d.Dispose());
            _disposables.Clear();
            _viewModel = null;
        }

        public void Die(bool value)
        {
            if (value)
                gameObject.SetActive(false);
        }

        protected virtual void SetPosition(Vector3 position) => transform.position = position;
        protected virtual void OnTriggerEnter2D(Collider2D other) => OnTrigger?.Invoke(other);

        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            gameObject.SetActive(true);
        }
    }
}