using Brodee.Triggers;

namespace Brodee.Handlers
{
    public class CheckBoxAttemptHandler : Handler
    {
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            if(!next.OptionMenuControls.IsCheckBoxAvailable())
                return EmptyTriggers;

            var checkboxCopy = next.OptionMenuControls.CreateCheckboxCopy();

            return EmptyTriggers;
        }
    }
}