using System;
using Brodee.Controls;
using Brodee.Core.Handlers;
using Brodee.Modules;
using Brodee.Triggers;

namespace Brodee.Core
{
    public class CoreModuleInstaller : ModuleInstaller
    {
        private readonly GameMenuControls _gameMenuControls;
        private readonly OptionMenuControls _optionMenuControls;
        private readonly GeneralControls _generalControls;
        private readonly Func<GameState> _oldGameStateFunc;
        private readonly Func<GameState> _newGameStateFunc;

        public CoreModuleInstaller(GameMenuControls gameMenuControls,
            OptionMenuControls optionMenuControls,
            GeneralControls generalControls,
            Func<GameState> oldGameStateFunc,
            Func<GameState> newGameStateFunc
            ) : base("Core")
        {
            _gameMenuControls = gameMenuControls;
            _optionMenuControls = optionMenuControls;
            _generalControls = generalControls;
            _oldGameStateFunc = oldGameStateFunc;
            _newGameStateFunc = newGameStateFunc;
        }

        public void Install(IHandlerHub handlerHub)
        {
            handlerHub.Register(new GameStateUpdateHandler(_oldGameStateFunc, _newGameStateFunc), HowOftenToProcess.EveryFrame,
                Scene.Hub | Scene.GamePlay | Scene.Collection | Scene.PackOpening | Scene.Tournament | Scene.Friendly | Scene.Draft | Scene.Adventure | Scene.TavernBrawl);

            handlerHub.RegisterOnTrigger<F12PressedTrigger>(new BrodeeOptionsMenuHandler(_optionMenuControls), Scene.Hub);
            handlerHub.RegisterOnTrigger<SliderAttemptTrigger>(new CardTileAttemptHandler(), Scene.Hub);
            handlerHub.RegisterOnTrigger<AddSettingsButtonTrigger>(new CreateSettingsButtonInGameMenuHandler(_gameMenuControls, _generalControls), Scene.Hub);
            handlerHub.Register(new CardHandGemColourChangeHandler(), HowOftenToProcess.EverySecond, Scene.GamePlay);
            handlerHub.Register(new CardCollectionGemColourChangeHandler(), HowOftenToProcess.EverySecond, Scene.Collection);
        }
    }
}