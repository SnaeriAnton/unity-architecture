using Core.Pool;
using UnityEngine;

namespace Game
{
    public class BowView : WeaponView
    {
        [SerializeField] private ArrowView _arrowTemplate;

        public ArrowView Shoot(float angle) => _poolManager.Spawn(_arrowTemplate, transform.position, Quaternion.Euler(0f, 0f, angle));
    }
}