using System;
using System.Collections.Generic;

namespace Core.GSystem
{
    public class Main : IDisposable
    {
        private readonly Dictionary<Type, object> _services = new();

        public void Register<T>(T instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            _services[typeof(T)] = instance;
        }

        public T Resolve<T>() where T : class
        {
            if (TryResolve(out T instance))
                return instance;

            throw new InvalidOperationException($"Service not registered: {typeof(T).FullName}");
        }

        public bool TryResolve<T>(out T instance) where T : class
        {
            if (_services.TryGetValue(typeof(T), out object obj) && obj is T casted)
            {
                instance = casted;
                return true;
            }

            instance = null;
            return false;
        }

        public void Dispose()
        {
            foreach (KeyValuePair<Type, object> kv in _services)
                if (kv.Value is IDisposable d)
                    d.Dispose();

            _services.Clear();
        }
    }
}