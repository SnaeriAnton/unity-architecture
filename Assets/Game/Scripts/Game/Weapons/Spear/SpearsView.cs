using UnityEngine;

namespace Game
{
    public class SpearsView : WeaponView
    {
        [SerializeField] private SpearView _spearTemplate;

        public SpearView Shoot(float angle) => _poolManager.Spawn(_spearTemplate, transform.position, Quaternion.Euler(0f, 0f, angle));
    }
}