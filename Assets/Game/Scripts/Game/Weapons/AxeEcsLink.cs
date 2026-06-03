using UnityEngine;

namespace Game
{
    public class AxeEcsLink : MonoBehaviour
    {
        public int Entity { get; private set; }
        public bool IsRegistered { get; private set; }

        public void Bind(int entity)
        {
            Entity = entity;
            IsRegistered = true;
        }

        public void Clear()
        {
            Entity = default;
            IsRegistered = false;
        }
    }
}