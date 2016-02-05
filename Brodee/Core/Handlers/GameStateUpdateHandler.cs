using System;
using Brodee.Components;
using Brodee.Controls;

namespace Brodee.Core.Handlers
{
    public class GameStateUpdateHandler : Handler
    {
        private Func<GameState> _newGameStateFunc;
        private Func<GameState> _oldGameStateFunc;

        public GameStateUpdateHandler(Func<GameState> _oldGameStateFunc, Func<GameState> _newGameStateFunc)
        {
            this._oldGameStateFunc = _oldGameStateFunc;
            this._newGameStateFunc = _newGameStateFunc;
        }

        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            var newGameState = _newGameStateFunc();
            var oldGameState = _oldGameStateFunc();

            var newSceneMode = SceneMgr.Get().GetMode().MapToScene();
            if (oldGameState.Mode != newSceneMode)
                Logger.AppendLine($"Changing scene to {newSceneMode}");

            newGameState.Mode = newSceneMode;
        }
    }

    public class StartUpHandler : Handler
    {
        private readonly IGeneralControls _generalControls;

        public StartUpHandler(IGeneralControls generalControls)
        {
            _generalControls = generalControls;
        }

        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            _generalControls.MakeConfirmPopUp("Start Up", "Just some text when starting up!");
        }
    }
}