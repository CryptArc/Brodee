using Brodee.Components;
using Brodee.Controls;

namespace Brodee.Core.Handlers
{
    public class BrodeeOptionsMenuHandler : Handler
    {
        private IOptionMenuControls _optionMenuControls;

        public BrodeeOptionsMenuHandler(IOptionMenuControls optionMenuControls)
        {
            _optionMenuControls = optionMenuControls;
        }

        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            var settingsWindow = _optionMenuControls.CreateBareSettingWindow();
            settingsWindow.SetActive(false);


            var sliderCopy = _optionMenuControls.CreateSliderCopy();
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