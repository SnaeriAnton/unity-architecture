using System.Collections.Generic;
using Contracts;
using UnityEngine;
using VContainer.Unity;

namespace Game
{
    public class RuntimeTickRegistry : IRuntimeTickRegistry, ITickable
    {
        private readonly List<IRuntimeTickable> _tickables = new();
        private readonly List<IRuntimeTickable> _toAdd = new();
        private readonly List<IRuntimeTickable> _toRemove = new();

        private bool _isTicking;

        public void Tick()
        {
            _isTicking = true;

            for (int i = 0; i < _tickables.Count; i++)
                _tickables[i].Tick(Time.deltaTime);

            _isTicking = false;

            if (_toRemove.Count > 0)
            {
                for (int i = 0; i < _toRemove.Count; i++)
                    _tickables.Remove(_toRemove[i]);

                _toRemove.Clear();
            }

            if (_toAdd.Count > 0)
            {
                for (int i = 0; i < _toAdd.Count; i++)
                    _tickables.Add(_toAdd[i]);

                _toAdd.Clear();
            }
        }

        public void Add(IRuntimeTickable runtimeTickable)
        {
            if (_isTicking)
            {
                _toAdd.Add(runtimeTickable);
                return;
            }

            _tickables.Add(runtimeTickable);
        }

        public void Remove(IRuntimeTickable runtimeTickable)
        {
            if (_isTicking)
            {
                _toRemove.Add(runtimeTickable);
                return;
            }

            _tickables.Remove(runtimeTickable);
        }
    }
}