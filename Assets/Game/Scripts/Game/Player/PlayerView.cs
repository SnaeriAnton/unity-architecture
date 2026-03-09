using System;
using Contracts;
using UnityEngine;

namespace Game
{
    public class PlayerView : MonoBehaviour, ITarget
    {
        [SerializeField] private SpriteRenderer _eyesSpriteRenderer;

        private PlayerModel _playerModel;

        public Transform Transform => transform;
        public bool IsDead => _playerModel.IsDead;

        public event Action<Coin> OnTouchedCoin;
        public event Action<Crystal> OnTouchedCrystal;

        public void Construct(PlayerModel playerModel)
        {
            _playerModel = playerModel;
            _playerModel.OnDied += Die;
            _playerModel.OnRestart += Restart;
        }

        public void Dispose()
        {
            _playerModel.OnDied -= Die;
            _playerModel.OnRestart -= Restart;
        }

        public void TakeDamage(int damage) => _playerModel.TakeDamage(damage);

        public void Restart()
        {
            transform.position = Vector3.zero;
            _eyesSpriteRenderer.enabled = true;
            _playerModel.Restart();
        }

        private void Die() => _eyesSpriteRenderer.enabled = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Coin coin))
            {
                OnTouchedCoin?.Invoke(coin);
            }

            if (other.TryGetComponent(out Crystal crystal))
            {
                OnTouchedCrystal?.Invoke(crystal);
            }
        }
    }
}