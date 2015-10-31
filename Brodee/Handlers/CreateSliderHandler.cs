using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{
    public class CreateSliderHandler : Handler
    {
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            if (!next.OptionMenuControls.IsSliderAvailable())
                return EmptyTriggers;

            var sliderCopy = next.OptionMenuControls.CreateSliderCopy();

            return EmptyTriggers;
        }
    }
}