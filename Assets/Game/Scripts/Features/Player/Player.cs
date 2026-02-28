using System;
using Shared;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, ITarget, IKillTracker, IPlayerLifecycle, IUpgradable
    {
        [SerializeField] private SpriteRenderer _eyesSpriteRenderer;
        [SerializeField] private PlayerStats _stats;

        private IWeapon _weapon;
        private Action _onDiedCallback;
        private PlayerInputController _inputController;
        private PlayerMovement _movement;
        private SpartanStats _spartanStats;
        private bool _isPlaying = false;
        private float _invulnTimer;
        private int _currentHealth;

        public Transform Transform => transform;
        public Vector3 Position => transform.position;
        public bool IsDead => _currentHealth <= 0;
        
        private IShield Shield => _weapon.Shield;
        private bool IsInvulnerable => _invulnTimer > 0f;

        private void Update()
        {
            if (IsDead || !_isPlaying) return;

            _weapon.ApplyAll();

            if (IsInvulnerable)
                _invulnTimer -= Time.deltaTime;
        }

        public void Construct(Action onDiedCallback, IWeapon weapon, IWorldBounds board, IInput input)
        {
            _weapon = weapon;
            _onDiedCallback = onDiedCallback;
            _movement = new(transform, board, _stats.Speed);
            _inputController = new(_movement, input);
        }

        public void SetWeaponStats(Weapons name, WeaponStats stats)
        {
            _weapon.SetStats(name, stats);
            if (Shield != null) GameEvents.Player.RaiseShieldReloading(Shield.CurrentCoolDownCount, Shield.CoolDown);
        }

        public bool HasWeapon(Weapons name) => _weapon.HasWeapon(name);

        public void StartPlay()
        {
            _isPlaying = true;
            _inputController.Enable();
        }

        public void KillEnemy()
        {
            _weapon.Update();
            if (Shield != null) GameEvents.Player.RaiseShieldReloading(Shield.CurrentCoolDownCount, Shield.CoolDown);
        }

        public void Restart()
        {
            transform.position = Vector3.zero;
            _eyesSpriteRenderer.enabled = true;
            _weapon.Reset();
            _spartanStats = default;
            _invulnTimer = 0;
        }

        public void SetPlayerStats(SpartanStats stats)
        {
            _spartanStats = stats;
            _currentHealth = _spartanStats.Health;
            GameEvents.Player.RaiseHealthCount(_currentHealth);
        }

        public void TakeDamage(int damage)
        {
            if (IsInvulnerable) return;
            if (Shield != null && Shield.TryApply())
            {
                GameEvents.Player.RaiseShieldReloading(Shield.CurrentCoolDownCount, Shield.CoolDown);
                return;
            }

            _invulnTimer = _stats.IFramesDuration;
            _currentHealth -= damage;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _spartanStats.Health);

            GameEvents.Player.RaiseHealthChanged(_currentHealth);
            if (_currentHealth == 0) Die();
        }

        private void Die()
        {
            _isPlaying = false;
            _inputController.Disable();
            _onDiedCallback.Invoke();

            _eyesSpriteRenderer.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IPickup pickup))
                pickup.PickUp();
        }
    }
}