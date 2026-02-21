using UnityEngine;
using Contracts;
using Core.GSystem;

namespace Core.InputSystem
{
    public class InputRoot : MonoBehaviour
    {
        private IInputProvider _provider;

        public IInput Input => _provider;
        
        private void Awake() => _provider = Application.isMobilePlatform ? new Mobile() : new PC();
        private void Update() => _provider?.Update();

    }
}