
using System.Collections.Generic;
using Runtime;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure
{
    public sealed class ObjectPool<T> : IResettablePool where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _root;
        private readonly Stack<T> _stack = new();
        private readonly List<T> _list = new();

        public ObjectPool(T prefab, Transform root)
        {
            _prefab = prefab;
            _root = root;
        }

        public T Spawn(int poolID,  Vector3 position, Quaternion quaternion)
        {
            T obj = _stack.Count > 0 ? _stack.Pop() : CreateNew();
            obj.transform.SetPositionAndRotation(position, quaternion);

            if (obj.TryGetComponent(out IPoolable p))
                p.OnSpawned(poolID, () => Despawn(obj));

            _list.Add(obj);
            return obj;
        }

        public void Despawn(T obj)
        {
            if (!_list.Remove(obj)) return;
            
            if (obj.TryGetComponent(out IPoolable p))
                p.OnDespawned();
            
            obj.transform.SetParent(_root, false);
            _stack.Push(obj);
        }

        public void Reset()
        {
            for (int i = _list.Count - 1; i >= 0; i--)
            {
                T obj = _list[i];

                if (obj.TryGetComponent(out IPoolable p))
                    p.OnDespawned();

                _stack.Push(obj);
            }

            _list.Clear();
        }

        private T CreateNew()
        {
            T obj = Object.Instantiate(_prefab, _root);
            return obj;
        }
    }
}