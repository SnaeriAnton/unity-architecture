using UnityEngine;

namespace Shared
{
    public interface IShield
    {
        public int CurrentCoolDownCount { get; }
        public int CoolDown { get; }

        public bool TryApply();
    }
}
