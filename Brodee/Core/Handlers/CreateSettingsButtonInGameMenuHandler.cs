using Brodee.Components;
using Brodee.Controls;
using Brodee.Triggers;

namespace Brodee.Core.Handlers
{
    public class CreateSettingsButtonInGameMenuHandler : TriggerHandler<GameMenuOpenedTrigger>
    {
        private readonly GameMenuControls _gameMenuControls;
        private readonly GeneralControls _generalControls;
        private readonly HandlerHub _handlerHub;

        public CreateSettingsButtonInGameMenuHandler(GameMenuControls gameMenuControls, GeneralControls generalControls, HandlerHub handlerHub)
        {
            _gameMenuControls = gameMenuControls;
            _generalControls = generalControls;
            _handlerHub = handlerHub;
        }

        public override void SpecificHandle(GameMenuOpenedTrigger trigger, IGameState next)
        {
            if (!_gameMenuControls.IsButtonContainerAvailable())
            {
                Logger.AppendLine("IsButtonContainerAvailable returned false");
                return;
            }
            _gameMenuControls.AddBrodeeSettingsButton(@event =>
            {
                _handlerHub.AddTrigger(new OpenBrodeeMenuTrigger());
            });
            _gameMenuControls.LayoutGameMenuBackground();
        }
    }
}