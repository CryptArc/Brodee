using UnityEngine;

namespace Brodee.Handlers
{
    public class BrodeeOptionsMenuHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            var settingsWindow = next.OptionMenuControls.CreateBareSettingWindow();

            var sliderCopy = next.OptionMenuControls.CreateSliderCopy();
            var scrollbarControl = sliderCopy.GetComponent<ScrollbarControl>();
            scrollbarControl.SetUpdateHandler(val =>
            {
                Logger.AppendLine("Changing scrollbarControl copy");
            });
            var gs = next as GameState;
            var uiCamGo = gs.GetUiCameraGameObject();
            uiCamGo.transform.localScale = new Vector3(1f, 1f, 1f);

            scrollbarControl.transform.SetParent(uiCamGo.transform);
            EditableInterface.SetGameObject(scrollbarControl.gameObject);

            GameObject go = scrollbarControl.gameObject.GetParentGameObject();
            while (go != null)
            {
                Logger.AppendLine($"BrodeeOptionsMenuHandler go.name parent:{go.name}");

                var parentGo = scrollbarControl.gameObject.GetParentGameObject();
                if (parentGo == go)
                    break;
                go = parentGo;
            }

            var sliderCopyText = sliderCopy.GetChildObjectContainingName("MusicVolumeLabel").GetComponent<UberText>();
            sliderCopyText.Text = "Slider Copy Text";
            sliderCopyText.UpdateText();

        }
    }
}