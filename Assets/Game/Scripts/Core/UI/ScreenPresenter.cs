using System;
using Zenject;

namespace Core.UI
{
    public class ScreenPresenter : IInitializable, IDisposable
    {
        public virtual void Show() { }
        public virtual void Hide() { }

        public virtual void Dispose() { }
        public virtual void Reset() { }
        public virtual void Initialize() { }
    }
}