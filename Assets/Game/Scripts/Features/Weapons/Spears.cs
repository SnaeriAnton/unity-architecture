using UnityEngine;

namespace Weapons
{
    public class Spears : Weapon
    {
        [SerializeField] private Spear _spearTemplate;
        
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
                    Spear spear = _pool.Spawn(_spearTemplate, transform.position, Quaternion.Euler(0f, 0f, angle));
                    spear.Init(_stats, direction);
                }
            }
        }
    }
}