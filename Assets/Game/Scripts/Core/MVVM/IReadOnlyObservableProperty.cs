using System;

namespace Core.MVVM
{
    public interface IReadOnlyObservableProperty<T>
    {
        public T Value { get; }

        public IDisposable Subscribe(Action<T> listener, bool notifyImmediately = true);
    }
}