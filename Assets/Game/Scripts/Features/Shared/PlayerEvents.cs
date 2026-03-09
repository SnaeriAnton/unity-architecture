using System;

namespace Shared
{
    public class PlayerEvents
    {
        public event Action<int> HealthChanged;
        public event Action<float, float> ShieldReloading;
        public event Action<int> ChangeHealthCount;

        public void RaiseHealthChanged(int current) => HealthChanged?.Invoke(current);
        public void RaiseHealthCount(int maxHealth) => ChangeHealthCount?.Invoke(maxHealth);
        public void RaiseShieldReloading(float current, float coolDown) => ShieldReloading?.Invoke(current, coolDown);
    }
}