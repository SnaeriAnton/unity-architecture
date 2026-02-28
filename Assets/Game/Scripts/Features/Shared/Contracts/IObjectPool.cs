using UnityEngine;

namespace Shared
{
    public interface IObjectPool
    {
        public T Spawn<T>(T prefab, Vector3 pos, Quaternion qua) where T : Component, IPoolable;
        public void Despawn<T>(T obj) where T : Component, IPoolable;
        public void Reset();
    }
}
