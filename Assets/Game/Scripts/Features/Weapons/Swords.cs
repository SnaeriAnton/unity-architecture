using System.Collections.Generic;
using UnityEngine;
using Shared;

namespace Weapons
{
    public class Swords : Weapon
    {
        private const float START_ANGLE_DEG = 0f;
        private const float SPRITE_ANGLE_OFFSET_DEG = 180f;

        private readonly List<Sword> _swords = new();
        private readonly Queue<Sword> _swordsQueue = new();

        [SerializeField] private Sword _swordTemplate;
        [SerializeField] private float _radius = 2f;

        public override void SetStats(WeaponStats stats)
        {
            base.SetStats(stats);
            AddSwords();
            LayoutChildren();
        }

        public override void Apply() => transform.Rotate(0f, 0f, -90f * _stats.RoundSpeed * Time.deltaTime, Space.Self);

        public override void Reset()
        {
            base.Reset();
            Clear();
        }

        private void AddSwords()
        {
            for (int i = _swords.Count; i < _stats.Count; i++) GetSword();
            
            _swords.ForEach(s => s.SetDamage(_stats.Damage));
        }

        private void LayoutChildren()
        {
            int count = _swords.Count;
            if (count == 0) return;

            float step = 360f / count;

            for (int i = 0; i < _swords.Count; i++)
            {
                Transform child = _swords[i].transform;

                float angleDeg = START_ANGLE_DEG + step * i;
                float rad = angleDeg * Mathf.Deg2Rad;

                child.localPosition = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * _radius;

                Vector2 tangent = new Vector2(-Mathf.Sin(rad), Mathf.Cos(rad));
                float z = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;

                child.localRotation = Quaternion.Euler(0f, 0f, z + SPRITE_ANGLE_OFFSET_DEG);
            }
        }

        private void Clear()
        {
            foreach (Sword sword in _swords)
            {
                sword.gameObject.SetActive(false);
                _swordsQueue.Enqueue(sword);
            }

            _swords.Clear();
        }

        private Sword GetSword()
        {
            if (!_swordsQueue.TryDequeue(out Sword sword))
                sword = Instantiate(_swordTemplate, transform);

            sword.gameObject.SetActive(true);
            _swords.Add(sword);
            return sword;
        }
    }
}