using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{
    public class CreateSliderHandler : Handler
    {
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            if (OptionsMenu.Get()?.m_musicVolume == null)
                return EmptyTriggers;

            var soundSlider = OptionsMenu.Get()?.m_musicVolume;
            
            var soundSliderOrigGameObject = soundSlider.gameObject;

            Logger.AppendLine($"soundSliderPos:{soundSliderOrigGameObject.transform.position}");
            Logger.AppendLine($"soundSliderLocalPos:{soundSliderOrigGameObject.transform.localPosition}");

            var copy = Object.Instantiate(soundSliderOrigGameObject);
            copy.transform.localPosition = soundSliderOrigGameObject.transform.position;

            return EmptyTriggers;
        }
    }
}