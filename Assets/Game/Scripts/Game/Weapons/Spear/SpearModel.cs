using System;
using UnityEngine;

namespace Game
{
    public class SpearModel : ProjectileModel
    {
        public SpearModel(WeaponStats stats, Vector3 direction) : base(stats)
        {
            CurrentStrength = Stats.Strength;
            Direction = direction;
        }
        
        public Vector3 Direction { get; private set; }
        public int CurrentStrength { get; private set; }
        
        public event Action OnStateChanged;
        public event Action OnDisable;
        
        public void ChangeStrength() => CurrentStrength--;
        public void Disable() => OnDisable?.Invoke();
        public void Move() => OnStateChanged?.Invoke();
    }
}