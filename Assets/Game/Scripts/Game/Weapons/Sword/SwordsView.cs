using System.Collections.Generic;
using Contracts;
using Core.Pool;
using UnityEngine;

namespace Game
{
    public class SwordsView : WeaponView
    {
        private const float START_ANGLE_DEG = 0f;
        private const float SPRITE_ANGLE_OFFSET_DEG = 180f;

        private readonly List<Sword> _swords = new();
        private readonly Queue<Sword> _swordsQueue = new();

        [SerializeField] private Sword _swordTemplate;
        [SerializeField] private float _radius = 2f;

        public override void Init(WeaponModel model)
        {
            base.Init(model);
            ((SwordsModel)_model).OnRotationChanged += RotateVisuals;
        }
        
        public override void Dispose()
        {
            base.Dispose();
            ((SwordsModel)_model).OnRotationChanged -= RotateVisuals;
        }

        protected override void SetStats()
        {
            base.SetStats();
            AddSwords();
            LayoutChildren();
        }

        protected override void Reset()
        {
            base.Reset();
            Clear();
        }
        
        private void RotateVisuals(float angle) => transform.Rotate(0f, 0f, angle, Space.Self);
        private void HandleSwordHit(IEnemyTarget target) => target.TakeDamage(_model.Stats.Damage);

        private void AddSwords()
        {
            for (int i = _swords.Count; i < _model.Stats.Count; i++) GetSword();
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
                sword.OnSwordHit -= HandleSwordHit;
            }

            _swords.Clear();
        }

        private Sword GetSword()
        {
            if (!_swordsQueue.TryDequeue(out Sword sword))
                sword = Instantiate(_swordTemplate, transform);

            sword.gameObject.SetActive(true);
            _swords.Add(sword);
            sword.OnSwordHit += HandleSwordHit;
            return sword;
        }
    }
}