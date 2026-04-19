using VContainer.Unity;
using Core.UI;

namespace Game
{
    public class GameStartup : IStartable
    {
        private IUIService _ui;

        public GameStartup(IUIService ui) => _ui = ui;

        public void Start() => _ui.ShowScreen<MenuScreenView>();
    }
}