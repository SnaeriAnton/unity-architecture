using System;

namespace Core.MVVM
{
    public class Command
    {
        private Action _action;

        public Command(Action action) => _action = action;
        
        public void Execute() => _action?.Invoke();
    }

    public class Command<T>
    {
        private Action<T> _action;
        
        public Command(Action<T> action) => _action = action;
        
        public void Execute(T value) => _action?.Invoke(value);
    }
}