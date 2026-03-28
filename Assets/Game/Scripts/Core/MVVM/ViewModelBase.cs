using System;
using System.Collections.Generic;

namespace Core.MVVM
{
    public class ViewModelBase : IDisposable
    {
        private readonly List<IDisposable> _disposables = new();

        public void AddDisposable(IDisposable disposable)
        {
            if (disposable != null)
                _disposables.Add(disposable);
        }

        public virtual void Dispose()
        {
            _disposables.ForEach(disposable => disposable.Dispose());
            _disposables.Clear();
        }
    }
}