using System;
using UnityEngine;

namespace Shared
{
    public interface IInput
    {
        public event Action<Vector2> OnClickDown;
        public event Action<Vector2> OnClickHold;
        public event Action<Vector2> OnClickUp;
        public event Action<Vector2> OnAxis;
        
        public void SetActivate(bool activate);
    }
}