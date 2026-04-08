using System;

namespace Contracts
{
    public interface IUpdateStream
    {
        public IObservable<float> OnUpdate { get; }
    }
}