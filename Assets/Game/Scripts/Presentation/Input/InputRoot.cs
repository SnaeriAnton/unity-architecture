using UnityEngine;
using Infrastructure;

namespace Presentation
{
    public class InputRoot : MonoBehaviour
    {
        private IInputProvider _provider;

        public IInput Input => _provider;

        private void Update() => _provider?.Update();

        public void Construct(bool isMobile) => _provider = isMobile ? new Mobile() : new PC();
    }
}