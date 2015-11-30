using Brodee.Triggers;

namespace Brodee.Handlers
{
    public class CheckBoxAttemptHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            if (!next.OptionMenuControls.IsCheckBoxAvailable())
                return;

            var checkboxCopy = next.OptionMenuControls.CreateCheckboxCopy();
            
        }
    }
}