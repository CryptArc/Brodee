using Brodee.Components;
using Brodee.Controls;

namespace Brodee.HandlersDump
{
    public class CreateSliderHandler : Handler
    {
        private readonly OptionMenuControls _optionMenuControls;

        public CreateSliderHandler(OptionMenuControls optionMenuControls)
        {
            _optionMenuControls = optionMenuControls;
        }

        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            if (!_optionMenuControls.IsSliderAvailable())
                return;

            var sliderCopy = _optionMenuControls.CreateSliderCopy();

        }
    }
}