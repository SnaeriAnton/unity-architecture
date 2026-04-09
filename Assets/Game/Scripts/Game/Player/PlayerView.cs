using UnityEngine;
using R3;

namespace Game
{
    public class PlayerView : MonoBehaviour
    {
        private readonly Subject<Collider2D> _onTrigger = new();
        
        [SerializeField] private SpriteRenderer _eyesSpriteRenderer;

        private CompositeDisposable _disposable = new();
        private PlayerViewModel _viewModel;

        public Vector3 Position => transform.position;
        public Vector2 HalfSize => new(transform.localScale.x * 0.5f, transform.localScale.y * 0.5f);
        public Observable<Collider2D> OnTrigger => _onTrigger;
        
        public void Bind(PlayerViewModel viewModel)
        {
            Unbind();
            _disposable = new();
            _viewModel = viewModel;

            _viewModel.Position.Subscribe(SetPosition).AddTo(_disposable);
            _viewModel.IsDead.Subscribe(RenderEyes).AddTo(_disposable);
        }

        public void Unbind()
        {
            _disposable.Dispose();
            _viewModel = null;
        }

        public void SetPosition(Vector3 position) => transform.position = position;
        private void RenderEyes(bool isDead) => _eyesSpriteRenderer.enabled = !isDead;
        private void OnTriggerEnter2D(Collider2D other) => _onTrigger.OnNext(other);
    }
}