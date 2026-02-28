using System;

namespace Shared
{
    public class ProgressionEvents
    {
        public event Action<int, int> ExpChanged;
        public event Action UpgradeMenuRequested;
        public event Action<bool> PauseRequested;
        public event Action UpgradeCompleted;
        
        public void RaiseUpgradeCompleted() => UpgradeCompleted?.Invoke();
        public void RaiseExpChanged(int current, int max) => ExpChanged?.Invoke(current, max);
        public void RaiseUpgradeMenuRequested() => UpgradeMenuRequested?.Invoke();
        public void RaisePauseRequested(bool paused) => PauseRequested?.Invoke(paused);
    }
}