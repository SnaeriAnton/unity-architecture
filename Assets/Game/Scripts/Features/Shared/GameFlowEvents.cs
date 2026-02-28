using System;

namespace Shared
{
    public enum GameScreen
    {
        Menu,
        Hud,
        Lose
    }

    public class GameFlowEvents
    {
        public event Action<GameScreen> ScreenRequested;
        public event Action GameStarted;
        public event Action GameRestarted;
        public event Action GameRestart;

        public void RaiseScreenRequested(GameScreen screen) => ScreenRequested?.Invoke(screen);
        public void RaiseGameStarted() => GameStarted?.Invoke();
        public void RaiseGameRestarted() => GameRestarted?.Invoke();
        public void RaiseGameRestart() => GameRestart?.Invoke();
    }
}