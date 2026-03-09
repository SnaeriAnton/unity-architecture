using System;

namespace Game
{
    public class SwordsModel : WeaponModel
    {
        public event Action<float> OnRotationChanged;
        
        public void SetCurrentAngle(float angle)
        {
            OnRotationChanged?.Invoke(angle);
        }
    }
}