using R3;

namespace Contracts
{
    public interface IUpdateStream
    {
        public Observable<float> OnUpdate { get; }
    }
}