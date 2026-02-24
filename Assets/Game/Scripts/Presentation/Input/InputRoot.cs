using UnityEngine;
using Application;
using Infrastructure;
using Runtime;

namespace Presentation
{
    public class InputRoot : MonoBehaviour
    {
        private IInputProvider _provider;

        public IInput Input => _provider;
        public IRuntimeInput RuntimeInput;

        private void Update() => _provider?.Update();

        public void Construct(bool isMobile) => RuntimeInput = _provider = isMobile ? new Mobile() : new PC();
    }
}