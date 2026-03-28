using UnityEngine;

namespace Game
{
    public class Spears : Weapon
    {
        [SerializeField] private Spear _spearTemplate;
        
        private float _attackTimer;
        
        public override void Tick(float dt)
        {
            if (_stats.Equals(default)) return;
            
            _attackTimer += dt;

            if (_attackTimer >= _stats.AttacksPerSecond)
            {
                _attackTimer = 0;
                
                for (int i = 0; i < _stats.Count; i++)
                {
                    Vector2 direction = Random.insideUnitCircle.normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Spear spear = _poolManager.Spawn(_spearTemplate, transform.position, Quaternion.Euler(0f, 0f, angle));
                    spear.Init(_loop, _stats, direction);
                    _loop.Add(spear);
                }
            }
        }
    }
}