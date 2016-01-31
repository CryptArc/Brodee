using UnityEngine;

namespace Brodee.Handlers
{
    public class BrodeeOptionsMenuHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            var settingsWindow = next.OptionMenuControls.CreateBareSettingWindow();
            settingsWindow.SetActive(false);
            

            var sliderCopy = next.OptionMenuControls.CreateSliderCopy();
            var scrollbarControl = sliderCopy.GetComponent<ScrollbarControl>();
            scrollbarControl.SetUpdateHandler(val =>
            {
                Logger.AppendLine("Changing scrollbarControl copy");
            });
            
            EditableInterface.Register(scrollbarControl.gameObject);

            var sliderCopyText = sliderCopy.GetChildObjectContainingName("MusicVolumeLabel").GetComponent<UberText>();
            sliderCopyText.Text = "Slider Copy Text";
            sliderCopyText.UpdateText();

        }
    }
}