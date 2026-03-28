using System.Collections.Generic;
using UnityEngine;
using ExtensionSystems;

namespace Core.Pool
{
    public sealed class PoolManager : IPoolService
    {
        private readonly Dictionary<int, IResettablePool> _pools = new();

        private Transform _root = new GameObject("[Pool]").transform;

        public void Reset() => _pools.Values.ForEach(p => p.Reset());

        public T Spawn<T>(T prefab, Vector3 pos, Quaternion qua) where T : Component, IPoolable
        {
            if (!_root) _root = new GameObject("[Pool]").transform;

            ObjectPool<T> pool = GetPool(prefab);
            return pool.Spawn(prefab.GetInstanceID(), pos, qua);
        }

        public void Despawn<T>(T obj) where T : Component, IPoolable => ((ObjectPool<T>)_pools[obj.PoolID]).Despawn(obj);

        private ObjectPool<T> GetPool<T>(T prefab) where T : Component
        {
            if (_pools.TryGetValue(prefab.GetInstanceID(), out IResettablePool existing))
                return (ObjectPool<T>)existing;

            ObjectPool<T> pool = new(prefab, _root);
            _pools[prefab.GetInstanceID()] = pool;
            return pool;
        }
    }
}