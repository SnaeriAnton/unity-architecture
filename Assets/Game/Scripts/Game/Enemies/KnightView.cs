using System;
using UnityEngine;

namespace Game
{
    public class KnightView : EnemyView
    {
        public event Action<Collider2D> OnTriggerExit;
        
        private void OnTriggerExit2D(Collider2D other) => OnTriggerExit?.Invoke(other);
    }
}