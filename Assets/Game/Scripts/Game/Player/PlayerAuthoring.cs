using UnityEngine;

namespace Game
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField] private PlayerStats _stats;

        public Transform Transform => transform;
        public float Speed => _stats.Speed;
        public float IFramesDuration => _stats.IFramesDuration;
    }
}