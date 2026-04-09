using R3;
using UnityEngine;

namespace Contracts
{
    public interface IInput
    {
        public Observable<Vector2> OnClickDown { get; }
        public Observable<Vector2> OnClickHold { get; }
        public Observable<Vector2> OnClickUp { get; }
        public Observable<Vector2> OnAxis { get; }
        
        public void SetActivate(bool activate);
    }
}