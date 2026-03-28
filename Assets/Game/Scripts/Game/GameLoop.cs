using System.Collections.Generic;
using Contracts;
using UnityEngine;

namespace Game
{
    public class GameLoop : IGameLoop
    {
        private readonly List<ITickable> _tickables = new();
        private readonly List<ITickable> _toAdd = new();
        private readonly List<ITickable> _toRemove = new();

        private bool _isTicking;

        public void Tick(float dt)
        {
            _isTicking = true;

            for (int i = 0; i < _tickables.Count; i++)
                _tickables[i].Tick(dt);

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

        public void Add(ITickable tickable)
        {
            if (_isTicking)
            {
                _toAdd.Add(tickable);
                return;
            }

            _tickables.Add(tickable);
        }

        public void Remove(ITickable tickable)
        {
            if (_isTicking)
            {
                _toRemove.Add(tickable);
                return;
            }

            _tickables.Remove(tickable);
        }
    }
}