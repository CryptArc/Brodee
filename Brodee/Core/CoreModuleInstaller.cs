using System;
using Brodee.Components;
using Brodee.Controls;
using Brodee.Core.Handlers;
using Brodee.Modules;
using Brodee.Triggers;

namespace Brodee.Core
{
    public class CoreModuleInstaller : ModuleInstaller
    {
        private readonly GameObjectRepo _gameObjectRepo;
        private readonly GameMenuControls _gameMenuControls;
        private readonly OptionMenuControls _optionMenuControls;
        private readonly GeneralControls _generalControls;
        private readonly Func<GameState> _oldGameStateFunc;
        private readonly Func<GameState> _newGameStateFunc;
        private readonly HandlerHub _handlerHub;

        public CoreModuleInstaller(GameObjectRepo gameObjectRepo,
            GameMenuControls gameMenuControls,
            OptionMenuControls optionMenuControls,
            GeneralControls generalControls,
            Func<GameState> oldGameStateFunc,
            Func<GameState> newGameStateFunc,
            HandlerHub handlerHub
            ) : base("Core")
        {
            _gameObjectRepo = gameObjectRepo;
            _gameMenuControls = gameMenuControls;
            _optionMenuControls = optionMenuControls;
            _generalControls = generalControls;
            _oldGameStateFunc = oldGameStateFunc;
            _newGameStateFunc = newGameStateFunc;
            _handlerHub = handlerHub;
        }

        public void Install(IHandlerHub handlerHub)
        {
            var allScenes = Scene.Hub | Scene.GamePlay | Scene.Collection | Scene.PackOpening | Scene.Tournament | Scene.Friendly | Scene.Draft | Scene.Adventure | Scene.TavernBrawl;
            handlerHub.Register(new GameStateUpdateHandler(_oldGameStateFunc, _newGameStateFunc), HowOftenToProcess.EveryFrame, allScenes);
            handlerHub.Register(new GameStateDifferHandler(_handlerHub), HowOftenToProcess.EveryFrame, allScenes);

            handlerHub.RegisterOnTrigger<OpenBrodeeMenuTrigger>(new BrodeeOptionsMenuHandler(_optionMenuControls, _gameObjectRepo), Scene.Hub);
            handlerHub.RegisterOnTrigger<SliderAttemptTrigger>(new CardTileAttemptHandler(), Scene.Hub);
            handlerHub.RegisterOnTrigger<GameMenuOpenedTrigger>(new CreateSettingsButtonInGameMenuHandler(_gameMenuControls, _generalControls, _handlerHub), Scene.Hub);

            handlerHub.Register(new CardHandGemColourChangeHandler(), HowOftenToProcess.EverySecond, Scene.GamePlay);
            handlerHub.Register(new CardCollectionGemColourChangeHandler(), HowOftenToProcess.EverySecond, Scene.Collection);
        }
    }
}