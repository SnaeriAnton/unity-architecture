using System;
using UnityEngine;

namespace Game
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _eyesSpriteRenderer;

        public Vector3 Position => transform.position;
        public Vector2 HalfSize => new(transform.localScale.x * 0.5f, transform.localScale.y * 0.5f);

        public event Action<Collider2D> OnTrigger;

        public void SetPosition(Vector3 position) => transform.position = position;

        public void ShowAlive() => _eyesSpriteRenderer.enabled = true;

        public void ShowDead() => _eyesSpriteRenderer.enabled = false;

        private void OnTriggerEnter2D(Collider2D other) => OnTrigger?.Invoke(other);
    }
}