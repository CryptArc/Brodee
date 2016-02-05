using Brodee.Components;
using Brodee.Controls;
using Brodee.HandlersDump;

namespace Brodee.Core.Handlers
{
    public class CreateSettingsButtonInGameMenuHandler : Handler
    {
        private readonly GameMenuControls _gameMenuControls;
        private readonly GeneralControls _generalControls;

        public CreateSettingsButtonInGameMenuHandler(GameMenuControls gameMenuControls, GeneralControls generalControls)
        {
            _gameMenuControls = gameMenuControls;
            _generalControls = generalControls;
        }

        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            if (!_gameMenuControls.IsButtonContainerAvailable())
            {
                Logger.AppendLine("IsButtonContainerAvailable returned false");
                return;
            }
            Logger.AppendLine("IsButtonContainerAvailable returned true");
            _gameMenuControls.AddBrodeeSettingsButton(@event =>
            {
                _generalControls.MakeConfirmPopUp("SettingsButton", "Just some text to confirm");
            });
            _gameMenuControls.LayoutGameMenuBackground();
        }
    }
}