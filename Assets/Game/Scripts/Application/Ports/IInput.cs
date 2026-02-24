using System;
using Domain;

namespace Application
{
    public interface IInput
    {
        public event Action<Vector2f> OnClickDown;
        public event Action<Vector2f> OnClickHold;
        public event Action<Vector2f> OnClickUp;
        public event Action<Vector2f> OnAxis;
        
        public void SetActivate(bool activate);
    }
}
