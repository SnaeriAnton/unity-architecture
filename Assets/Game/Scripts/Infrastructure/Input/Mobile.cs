using System;
using Domain;
using Runtime;

namespace Infrastructure
{
    public class Mobile : IInputProvider
    {
        private bool _isActive;
        
        public event Action<Vector2f> OnClickDown;
        public event Action<Vector2f> OnClickHold;
        public event Action<Vector2f> OnClickUp;
        public event Action<Vector2f> OnAxis;
        public event Action<float, float> Move;

        public void SetActivate(bool activate) => _isActive = activate;
        public void Update() { }
    }
}