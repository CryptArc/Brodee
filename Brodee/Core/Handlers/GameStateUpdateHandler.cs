using System;
using Brodee.Components;

namespace Brodee.Core.Handlers
{
    public class GameStateUpdateHandler : Handler
    {
        private readonly Func<GameState> _newGameStateFunc;
        private readonly Func<GameState> _oldGameStateFunc;

        public GameStateUpdateHandler(Func<GameState> oldGameStateFunc, Func<GameState> newGameStateFunc)
        {
            _oldGameStateFunc = oldGameStateFunc;
            _newGameStateFunc = newGameStateFunc;
        }

        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            var newGameState = _newGameStateFunc();
            var oldGameState = _oldGameStateFunc();

            var newSceneMode = SceneMgr.Get().GetMode().MapToScene();
            if (oldGameState.Mode != newSceneMode)
                Logger.AppendLine($"Changing scene to {newSceneMode}");

            newGameState.Mode = newSceneMode;
            newGameState.GameMenuOpen = GameMenu.Get()?.IsShown() ?? false;
            newGameState.OptionsMenuOpen = OptionsMenu.Get()?.IsShown() ?? false;
        }
    }
}