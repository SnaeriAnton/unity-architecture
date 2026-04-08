using System;
using UniRx;
using UnityEngine;

namespace Game
{
    public class KnightView : EnemyView
    {
        private readonly Subject<Collider2D> _onTriggerExit = new();
        
        public IObservable<Collider2D> OnTriggerExit => _onTriggerExit;
        
        private void OnTriggerExit2D(Collider2D other) => _onTriggerExit.OnNext(other);
    }
}