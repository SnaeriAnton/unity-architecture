using System;
using UnityEngine;

namespace Game
{
    public class ArrowModel : ProjectileModel
    {
        public ArrowModel(WeaponStats stats, Vector3 direction) : base(stats) => Direction = direction;

        public Vector3 Direction { get; private set; }

        public event Action OnStateChanged;
        public event Action OnDisable;

        public void Disable() => OnDisable?.Invoke();
        public void Move() => OnStateChanged?.Invoke();
    }
}