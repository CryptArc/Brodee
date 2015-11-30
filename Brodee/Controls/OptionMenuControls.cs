using Brodee.Handlers;
using UnityEngine;

namespace Brodee.Controls
{
    public interface IOptionMenuControls
    {
        bool IsSliderAvailable();
        bool IsCheckBoxAvailable();
        GameObject CreateCheckboxCopy();
        GameObject CreateSliderCopy();
        GameObject CreateBareSettingWindow();
    }

    public class OptionMenuControls : IOptionMenuControls
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
            var sliderCopy = Object.Instantiate(soundSliderOrigGameObject);
            sliderCopy.PopulateTransform(soundSliderOrigGameObject);
            return sliderCopy;
        }

        public GameObject CreateBareSettingWindow()
        {
            var optionsOriginal = OptionsMenu.Get().gameObject;

            var myOptions = new GameObject();
            myOptions.transform.localPosition = new Vector3(optionsOriginal.transform.position.x, optionsOriginal.transform.position.y, optionsOriginal.transform.position.z);
            myOptions.transform.localScale = new Vector3(optionsOriginal.transform.localScale.x, optionsOriginal.transform.localScale.y, optionsOriginal.transform.localScale.z);
            myOptions.transform.localRotation = new Quaternion(optionsOriginal.transform.localRotation.x, optionsOriginal.transform.localRotation.y, optionsOriginal.transform.localRotation.z, optionsOriginal.transform.localRotation.w);
            myOptions.transform.rotation = new Quaternion(optionsOriginal.transform.rotation.x, optionsOriginal.transform.rotation.y, optionsOriginal.transform.rotation.z, optionsOriginal.transform.rotation.w);
            myOptions.name = "My Clone OptionsClone";
            Logger.AppendLine($"optionsOriginal.transform.childCount:{optionsOriginal.transform.childCount}");
            for (int i = 0; i < optionsOriginal.transform.childCount; i++)
            {
                var newGameObject = GameUtils.Instantiate(optionsOriginal.transform.GetChild(i).gameObject) as GameObject;
                if (newGameObject == null)
                    continue;
                newGameObject.SetActive(true);
                newGameObject.layer = optionsOriginal.transform.GetChild(i).gameObject.layer;
                newGameObject.transform.SetParent(myOptions.transform);
                var origLocalPos = optionsOriginal.transform.GetChild(i).localPosition;
                var origLocalScale = optionsOriginal.transform.GetChild(i).localScale;
                var origLocalRotation = optionsOriginal.transform.GetChild(i).localRotation;
                var origRotation = optionsOriginal.transform.GetChild(i).rotation;
                newGameObject.transform.localPosition = new Vector3(origLocalPos.x, origLocalPos.y, origLocalPos.z);
                newGameObject.transform.localScale = new Vector3(origLocalScale.x, origLocalScale.y, origLocalScale.z);
                newGameObject.transform.localRotation = new Quaternion(origLocalRotation.x, origLocalRotation.y, origLocalRotation.z, origLocalRotation.w);
                newGameObject.transform.rotation = new Quaternion(origRotation.x, origRotation.y, origRotation.z, origRotation.w);
            }
            var windowContents = myOptions.GetChildObjectContainingName("WindowContents");
            windowContents.SetActive(false);

            return myOptions;
        }
    }
}