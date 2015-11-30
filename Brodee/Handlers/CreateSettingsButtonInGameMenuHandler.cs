namespace Brodee.Handlers
{
    public class CreateSettingsButtonInGameMenuHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            if (!next.GameMenuControls.IsButtonContainerAvailable())
            {
                Logger.AppendLine("IsButtonContainerAvailable returned false");
                return;
            }
            Logger.AppendLine("IsButtonContainerAvailable returned true");
            next.GameMenuControls.AddBrodeeSettingsButton(@event =>
            {
                next.GeneralControls.MakeConfirmPopUp("SettingsButton","Just some text to confirm");
            });
            next.GameMenuControls.LayoutGameMenuBackground();
        }
    }
}