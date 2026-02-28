using System;
using UnityEngine;

namespace Core.InputSystem
{
    internal class Mobile : IInputProvider
    {
        private bool _isActive;
        
        public event Action<Vector2> OnClickDown;
        public event Action<Vector2> OnClickHold;
        public event Action<Vector2> OnClickUp;
        public event Action<Vector2> OnAxis;

        public void SetActivate(bool activate) => _isActive = activate;
        public void Update() { }
    }
}