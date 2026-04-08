using System;
using UnityEngine;
using Contracts;
using UniRx;

namespace Core.InputSystem
{
    internal class Mobile : IInputProvider
    {
        private readonly Subject<Vector2> _onClickDown = new();
        private readonly Subject<Vector2> _onClickHold = new();
        private readonly Subject<Vector2> _onClickUp = new();
        private readonly Subject<Vector2> _onAxis = new();
        
        private bool _isActive;

        public IObservable<Vector2> OnClickDown => _onClickDown;
        public IObservable<Vector2> OnClickHold => _onClickHold;
        public IObservable<Vector2> OnClickUp => _onClickUp;
        public IObservable<Vector2> OnAxis => _onAxis;

        public void SetActivate(bool activate) => _isActive = activate;
        public void Update() { }
    }
}