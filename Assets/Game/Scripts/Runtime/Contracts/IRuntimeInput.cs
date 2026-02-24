using System;
using UnityEngine;

namespace Runtime
{
    public interface IRuntimeInput
    {
        event Action<float, float> Move;
    }
}
