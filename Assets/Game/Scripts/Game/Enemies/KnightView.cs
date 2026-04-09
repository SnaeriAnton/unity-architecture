using R3;
using UnityEngine;

namespace Game
{
    public class KnightView : EnemyView
    {
        private readonly Subject<Collider2D> _onTriggerExit = new();
        
        public Observable<Collider2D> OnTriggerExit => _onTriggerExit;
        
        private void OnTriggerExit2D(Collider2D other) => _onTriggerExit.OnNext(other);
    }
}