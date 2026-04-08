using System;
using UnityEngine;

namespace Contracts
{
    public interface IInput
    {
        public IObservable<Vector2> OnClickDown { get; }
        public IObservable<Vector2> OnClickHold { get; }
        public IObservable<Vector2> OnClickUp { get; }
        public IObservable<Vector2> OnAxis { get; }
        
        public void SetActivate(bool activate);
    }
}