using System;
using Domain;
using Runtime;
using UnityEngine;

namespace Infrastructure
{
    public class PC : IInputProvider, IRuntimeInput
    {
        private bool _isActive;

        public event Action<Vector2f> OnClickDown;
        public event Action<Vector2f> OnClickHold;
        public event Action<Vector2f> OnClickUp;
        public event Action<Vector2f> OnAxis;
        public event Action<float, float> Move;

        private Vector2f _axis = new();
        private Vector2f _mousePosition = new();
        
        public void SetActivate(bool activate) => _isActive = activate;

        public void Update()
        {
            if (!_isActive) return;

            if (Input.GetMouseButtonDown(0))
            {
                _mousePosition = new(Input.mousePosition.x, Input.mousePosition.y);
                OnClickDown?.Invoke(_mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                _mousePosition = new(Input.mousePosition.x, Input.mousePosition.y);
                OnClickHold?.Invoke(_mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _mousePosition = new(Input.mousePosition.x, Input.mousePosition.y);
                OnClickUp?.Invoke(_mousePosition);
            }

            _axis = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (_axis.X != 0 || _axis.Y != 0)
            {
                OnAxis?.Invoke(_axis);
                Move?.Invoke(_axis.X, _axis.Y);
            }
        }
    }
}