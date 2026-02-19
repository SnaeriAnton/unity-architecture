using System;
using UnityEngine;

namespace Core.InputSystem
{
    internal class PC : IInputProvider
    {
        private bool _isActive;

        public event Action<Vector2> OnClickDown;
        public event Action<Vector2> OnClickHold;
        public event Action<Vector2> OnClickUp;
        public event Action<Vector2> OnAxis;

        private Vector2 _axis = new();
        public void SetActivate(bool activate) => _isActive = activate;

        public void Update()
        {
            if (!_isActive) return;

            if (Input.GetMouseButtonDown(0)) OnClickDown?.Invoke(Input.mousePosition);
            if (Input.GetMouseButton(0)) OnClickHold?.Invoke(Input.mousePosition);
            if (Input.GetMouseButtonUp(0)) OnClickUp?.Invoke(Input.mousePosition);

            _axis.x = Input.GetAxisRaw("Horizontal");
            _axis.y = Input.GetAxisRaw("Vertical");

            if (_axis.x != 0 || _axis.y != 0)
                OnAxis?.Invoke(_axis);
        }
    }
}