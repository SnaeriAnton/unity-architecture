using System;
using UniRx;
using UnityEngine;

namespace Core.InputSystem
{
    internal class PC : IInputProvider
    {
        private readonly Subject<Vector2> _onClickDown = new();
        private readonly Subject<Vector2> _onClickHold = new();
        private readonly Subject<Vector2> _onClickUp = new();
        private readonly Subject<Vector2> _onAxis = new();
        
        private Vector2 _axis = new();
        private bool _isActive;

        public IObservable<Vector2> OnClickDown => _onClickDown;
        public IObservable<Vector2> OnClickHold => _onClickHold;
        public IObservable<Vector2> OnClickUp => _onClickUp;
        public IObservable<Vector2> OnAxis => _onAxis;
        
        
        public void SetActivate(bool activate) => _isActive = activate;

        public void Update()
        {
            if (!_isActive) return;

            if (Input.GetMouseButtonDown(0)) _onClickDown.OnNext(Input.mousePosition);
            if (Input.GetMouseButton(0)) _onClickHold.OnNext(Input.mousePosition);
            if (Input.GetMouseButtonUp(0)) _onClickUp.OnNext(Input.mousePosition);

            _axis.x = Input.GetAxisRaw("Horizontal");
            _axis.y = Input.GetAxisRaw("Vertical");

            _onAxis.OnNext(_axis);
        }
    }
}