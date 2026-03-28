using System;
using System.Collections.Generic;

namespace Core.MVVM
{
    public class ObservableProperty<T> : IReadOnlyObservableProperty<T>
    {
        private readonly List<Action<T>> _listeners = new();
        
        public ObservableProperty(T initialValue) => Value = initialValue;

        public T Value { get; private set; }

        public ObservableProperty() => Value = default;
        
        public void Set(T value, bool notifyObservers = false)
        {
            if (!notifyObservers && EqualityComparer<T>.Default.Equals(Value, value)) return;

            Value = value;

            for (int i = 0; i < _listeners.Count; i++)
                _listeners[i]?.Invoke(Value);
        }

        public IDisposable Subscribe(Action<T> listener, bool notifyImmediately = true)
        {
            if (listener == null)
                throw new ArgumentNullException(nameof(listener));

            _listeners.Add(listener);

            if (notifyImmediately)
                listener.Invoke(Value);

            return new Subscription(this, listener);
        }
        
        private void Unsubscribe(Action<T> listener)
        {
            _listeners.Remove(listener);
        }
        
        private sealed class Subscription : IDisposable
        {
            private ObservableProperty<T> _property;
            private Action<T> _listener;
            private bool _isDisposed;

            public Subscription(ObservableProperty<T> property, Action<T> listener)
            {
                _property = property;
                _listener = listener;
            }

            public void Dispose()
            {
                if (_isDisposed)
                    return;

                _isDisposed = true;
                _property?.Unsubscribe(_listener);
                _property = null;
                _listener = null;
            }
        }
    }
}