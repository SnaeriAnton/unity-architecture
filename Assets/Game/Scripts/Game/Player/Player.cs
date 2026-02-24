using System;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Core.GSystem;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _eyesSpriteRenderer;
        [SerializeField] private PlayerStats _stats;
        
        private WeaponSystem _weapon;
        private Action<Crystal> _onCrystalCallback;
        private Action _onDiedCallback;
        private PlayerInputController _inputController;
        private PlayerMovement _movement;
        private SpartanStats _spartanStats;
        private Wallet _wallet;
        private bool _isPlaying = false;
        private float _invulnTimer;

        public IReadOnlyDictionary<Weapons, Weapon> Weapons => _weapon.Weapons;
        public Shield Shield => _weapon.Shield;
        public int CurrentHealth { get; private set; }
        public int MaxHealth => _spartanStats.Health;
        public bool IsDead => CurrentHealth <= 0;
        
        private Wallet Wallet => _wallet ??= G.Main.Resolve<Wallet>();

        private bool IsInvulnerable => _invulnTimer > 0f;

        private void Update()
        {
            if (IsDead || !_isPlaying) return;

            _weapon.ApplyAll();

            if (IsInvulnerable)
                _invulnTimer -= Time.deltaTime;
        }

        public void Construct(Action<Crystal> onCrystalCallback, Action onDiedCallback)
        {
            _onCrystalCallback = onCrystalCallback;
            _onDiedCallback = onDiedCallback;
            _weapon = new();
            _movement = new(transform, _stats.Speed);
            _inputController = new(_movement);
        }
        
        public void AddWeapon(Weapons name, Weapon weapon) => _weapon.AddWeapon(name, weapon);
        public void SetWeaponStats(Weapons name, WeaponStats stats) => _weapon.SetStats(name, stats);

        public void StartPlay()
        {
            _isPlaying = true;
            _inputController.Enable();
        }

        public void KillEnemy()
        {
            _weapon.Update();
            G.Main.Resolve<IUIService>().GetScreen<HUD>().Refresh();
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
                G.Main.Resolve<IUIService>().GetScreen<HUD>().Refresh();
                return;
            }

            _invulnTimer = _stats.IFramesDuration;
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _spartanStats.Health);

            G.Main.Resolve<IUIService>().GetScreen<HUD>().Refresh();
            if (CurrentHealth == 0) Die();
        }

        private void AddCoins()
        {
            Wallet.AddCoin();
            G.Main.Resolve<IUIService>().GetScreen<HUD>().Refresh();
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
                AddCoins();
                coin.PickUp();
            }

            if (other.TryGetComponent(out Crystal crystal))
                _onCrystalCallback.Invoke(crystal);
        }
    }
}