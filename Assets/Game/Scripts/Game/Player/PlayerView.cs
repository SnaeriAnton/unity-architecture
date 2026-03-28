using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerView : MonoBehaviour
    {
        private readonly List<IDisposable> _disposables = new();
        
        [SerializeField] private SpriteRenderer _eyesSpriteRenderer;

        private PlayerViewModel _viewModel;

        public Vector3 Position => transform.position;
        public Vector2 HalfSize => new(transform.localScale.x * 0.5f, transform.localScale.y * 0.5f);

        public event Action<Collider2D> OnTrigger;
        
        public void Bind(PlayerViewModel viewModel)
        {
            Unbind();
            _viewModel = viewModel;

            _disposables.Add(_viewModel.Position.Subscribe(SetPosition));
            _disposables.Add(_viewModel.IsDead.Subscribe(RenderEyes));
        }

        public void Unbind()
        {
            _disposables.ForEach(d => d.Dispose());
            _disposables.Clear();
            _viewModel = null;
        }

        public void SetPosition(Vector3 position) => transform.position = position;
        private void RenderEyes(bool isDead) => _eyesSpriteRenderer.enabled = !isDead;
        private void OnTriggerEnter2D(Collider2D other) => OnTrigger?.Invoke(other);
    }
}