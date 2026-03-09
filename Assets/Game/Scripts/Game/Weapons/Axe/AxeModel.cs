using System;
using UnityEngine;

namespace Game
{
    public class AxeModel
    {
        public AxeStats Stats { get; }

        public AxeModel(AxeStats stats, Vector3 direction)
        {
            Stats = stats;
            Direction = direction;
        }

        public Vector3 Direction { get; private set; }

        public event Action OnStateChanged;
        public event Action OnDisable;

        public void Disable() => OnDisable?.Invoke();
        public void Move() => OnStateChanged?.Invoke();
    }
}