using UnityEngine;

namespace Brodee.Handlers
{
    public class OptionMenuControls
    {
        public bool IsSliderAvailable()
        {
            return OptionsMenu.Get()?.m_musicVolume != null;
        }

        public bool IsCheckBoxAvailable()
        {
            return OptionsMenu.Get()?.m_fullScreenCheckbox != null;
        }

        public GameObject CreateCheckboxCopy()
        {
            if (!IsCheckBoxAvailable())
                return null;

            var checkBoxOrigGameObject = OptionsMenu.Get()?.m_fullScreenCheckbox.gameObject;

            var copy = Object.Instantiate(checkBoxOrigGameObject) as GameObject;
            copy.transform.localPosition = checkBoxOrigGameObject.transform.position;

            return copy;
        }

        public GameObject CreateSliderCopy()
        {
            if (!IsSliderAvailable())
                return null;
            var soundSlider = OptionsMenu.Get()?.m_musicVolume;

            var soundSliderOrigGameObject = soundSlider.gameObject;

            var sliderCopy = Object.Instantiate(soundSliderOrigGameObject) as GameObject;
            sliderCopy.transform.localPosition = soundSliderOrigGameObject.transform.position;
            return sliderCopy;
        }
    }
}