using System;
using Contracts;
using UnityEngine;

namespace Game
{
    public class Sword : MonoBehaviour
    {
        public event Action<IEnemyTarget> OnSwordHit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEnemyTarget target))
                OnSwordHit?.Invoke(target);
        }
    }
}