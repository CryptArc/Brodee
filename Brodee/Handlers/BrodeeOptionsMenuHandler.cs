using Brodee.Triggers;
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
            Logger.AppendLine($"uiCamGo.transform.pos:{uiCamGo.transform.position}");
            Logger.AppendLine($"scrollbarControl.transform.pos:{scrollbarControl.transform.position}");
            Logger.AppendLine($"scrollbarControl.transform.scale:{scrollbarControl.transform.localScale}");

            scrollbarControl.transform.SetParent(uiCamGo.transform);
            scrollbarControl.gameObject.ZeroPositionTransform();
            Helper.LogGameObjectComponents(scrollbarControl.gameObject);
            EditableInterface.SetGameObject(scrollbarControl.gameObject);

            GameObject go = scrollbarControl.gameObject.GetParentGameObject();
            while (go != null)
            {
                Logger.AppendLine($"BrodeeOptionsMenuHandler go.name parent:{go.name}");

                go = scrollbarControl.gameObject.GetParentGameObject();
            }

            Logger.AppendLine($"scrollbarControl.transform.pos:{scrollbarControl.transform.position}");
            Logger.AppendLine($"scrollbarControl.transform.scale:{scrollbarControl.transform.localScale}");

            Helper.LogGameObjectComponents(settingsWindow);
            var header = settingsWindow.GetChildObjectContainingName("Header");
            Helper.LogGameObjectComponents(header);
            var headerUberText = header.GetChildObjectContainingName("UberText");
            headerUberText.GetComponent<UberText>().Text = "Test Text";
            headerUberText.GetComponent<UberText>().UpdateText();
            headerUberText.GetComponent<UberText>().UpdateNow();

            Helper.LogGameObjectComponents(sliderCopy);
            var sliderCopyText = sliderCopy.GetChildObjectContainingName("MusicVolumeLabel").GetComponent<UberText>();
            sliderCopyText.Text = "Slider Copy Text";
            sliderCopyText.UpdateText();

            sliderCopy.transform.localScale = new Vector3(40.0f, 1.0f, 40.0f);

            Logger.AppendLine($"sliderCopy scale {sliderCopy.transform.localScale}");
            Logger.AppendLine($"scrollbarControl scale {scrollbarControl.transform.localScale}");
            Logger.AppendLine($"scrollbarControl.m_Thumb scale {scrollbarControl.m_Thumb.transform.localScale}");

        }
    }
}