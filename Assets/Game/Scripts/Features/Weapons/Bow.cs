using UnityEngine;

namespace Weapons
{
    public class Bow : Weapon
    {
        [SerializeField] private Arrow _arrowTemplate;

        private float _lastTimeSpawn;

        public override void Apply()
        {
            if (_stats.Equals(default)) return;

            float interval = Time.time - _lastTimeSpawn;

            if (interval >= _stats.AttacksPerSecond)
            {
                _lastTimeSpawn = Time.time;
                for (int i = 0; i < _stats.Count; i++)
                {
                    Vector2 direction = Random.insideUnitCircle.normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Arrow arrow = _pool.Spawn(_arrowTemplate, transform.position, Quaternion.Euler(0f, 0f, angle));
                    arrow.Init(_stats, direction);
                }
            }
        }
    }
}