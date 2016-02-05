using Brodee.Components;
using Brodee.Controls;

namespace Brodee.HandlersDump
{
    public class CheckBoxAttemptHandler : Handler
    {
        private readonly OptionMenuControls _optionMenuControls;

        public CheckBoxAttemptHandler(OptionMenuControls optionMenuControls)
        {
            _optionMenuControls = optionMenuControls;
        }

        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            if (!_optionMenuControls.IsCheckBoxAvailable())
                return;

            var checkboxCopy = _optionMenuControls.CreateCheckboxCopy();

        }
    }
}