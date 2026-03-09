using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class SpearsModel : WeaponModel
    {
        public int CountSpears => Stats.Count;

        public (Vector2, float) GetCoordinates()
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return (direction, angle);
        }
    }
}
