using System;
using UnityEngine;
using Core.Pool;
using R3;

namespace Game
{
    public abstract class EnemyView : MonoBehaviour, IPoolable
    {
        private readonly Subject<Collider2D> _onTrigger = new();
        
        protected EnemyViewModel _viewModel;
        
        private CompositeDisposable _disposable = new();

        public Observable<Collider2D> OnTrigger => _onTrigger;
        public Vector3 Position => transform.position;
        public int PoolID { get; private set; }

        public virtual void Bind(EnemyViewModel viewModel)
        {
            Unbind();
            _disposable = new();
            _viewModel = viewModel;
            _viewModel.Position.Subscribe(SetPosition).AddTo(_disposable);
            _viewModel.IsDead.Subscribe(Die).AddTo(_disposable);
        }

        public virtual void Unbind()
        {
            _disposable.Dispose();
            _viewModel = null;
        }

        public void Die(bool value)
        {
            if (value)
                gameObject.SetActive(false);
        }

        protected virtual void SetPosition(Vector3 position) => transform.position = position;
        protected virtual void OnTriggerEnter2D(Collider2D other) => _onTrigger.OnNext(other);

        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            gameObject.SetActive(true);
        }
    }
}