
using System;
using System.Collections.Generic;
using Domain;
using UnityEngine;

namespace Runtime
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _eyesSpriteRenderer;

        private WeaponSystem _weapon;
        private Action _onCrystalCallback;
        private Action _onDiedCallback;
        private Action _onAddCoin;
        private Action _onTakeDamage;
        private PlayerInputController _inputController;
        private PlayerMovement _movement;
        private SpartanStats _spartanStats;
        private bool _isPlaying = false;
        private float _invulnTimer;
        public float _iFramesDuration;

        public IReadOnlyDictionary<Weapons, Weapon> Weapons => _weapon.Weapons;
        public Shield Shield => _weapon.Shield;
        public int CurrentHealth { get; private set; }
        public int MaxHealth => _spartanStats.Health;
        public bool IsDead => CurrentHealth <= 0;

        private bool IsInvulnerable => _invulnTimer > 0f;

        private void Update()
        {
            if (IsDead || !_isPlaying) return;

            _weapon.ApplyAll();

            if (IsInvulnerable)
                _invulnTimer -= Time.deltaTime;
        }

        public void Construct(Action onAddCoin, Action onCrystalCallback, Action onDiedCallback, Action onTakeDamage, Border border, IRuntimeInput input, float iFramesDuration, float speed)
        {
            _onAddCoin = onAddCoin;
            _onCrystalCallback = onCrystalCallback;
            _onDiedCallback = onDiedCallback;
            _onTakeDamage = onTakeDamage;
            _weapon = new();
            _movement = new(transform, border, speed);
            _inputController = new(_movement, input);
            _iFramesDuration = iFramesDuration;
        }

        public bool HasWeapon(Weapons weapon) => _weapon.Weapons.ContainsKey(weapon);
        public void AddWeapon(Weapons name, Weapon weapon) => _weapon.AddWeapon(name, weapon);
        public void SetWeaponStats(Weapons name, WeaponStats stats) => _weapon.SetStats(name, stats);
        public void KillEnemy() => _weapon.Update();

        public void StartPlay()
        {
            _isPlaying = true;
            _inputController.Enable();
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
            CurrentHealth = _spartanStats.Health;
        }

        public void TakeDamage(int damage)
        {
            if (IsInvulnerable) return;
            if (Shield && Shield.TryApply())
            {
                _onTakeDamage.Invoke();
                return;
            }

            _invulnTimer = _iFramesDuration;
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _spartanStats.Health);

            _onTakeDamage.Invoke();
            if (CurrentHealth == 0) Die();
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
            if (other.TryGetComponent(out Coin coin))
            {
                _onAddCoin.Invoke();
                coin.PickUp();
            }

            if (other.TryGetComponent(out Crystal crystal))
            {
                crystal.PickUp();
                _onCrystalCallback.Invoke();
            }
        }
    }
}