using Brodee.Triggers;

namespace Brodee.Handlers
{
    public class CreateSettingsButtonInGameMenuHandler : Handler
    {
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            if (!next.GameMenuControls.IsButtonContainerAvailable())
                return EmptyTriggers;

            next.GameMenuControls.AddBrodeeSettingsButton(@event =>
            {
                next.GeneralControls.MakeConfirmPopUp("SettingsButton","Just some text to confirm");
            });
            next.GameMenuControls.LayoutGameMenuBackground();

            return EmptyTriggers;
        }
    }
}