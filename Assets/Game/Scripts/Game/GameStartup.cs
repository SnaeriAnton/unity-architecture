using UnityEngine;
using Zenject;
using Core.UI;

namespace Game
{
    public class GameStartup : MonoBehaviour
    {
        private IUIService _ui;

        [Inject]
        public void Construct(IUIService ui) => _ui = ui;

        private void Start() => _ui.ShowScreen<MenuScreenView>();
    }
}