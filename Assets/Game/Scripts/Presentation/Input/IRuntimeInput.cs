using System;

namespace Presentation
{
    public interface IRuntimeInput
    {
        event Action<float, float> Move;
    }
}
