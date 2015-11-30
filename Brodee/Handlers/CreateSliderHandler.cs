using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{
    public class CreateSliderHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            if (!next.OptionMenuControls.IsSliderAvailable())
                return;

            var sliderCopy = next.OptionMenuControls.CreateSliderCopy();

        }
    }
}