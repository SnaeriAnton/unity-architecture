using Application;
using Runtime;

namespace Infrastructure
{
    public interface IInputProvider : IInput, IUpdatable, IRuntimeInput { }
}