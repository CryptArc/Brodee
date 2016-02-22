using Brodee.Components;
using Brodee.Controls;
using UnityEngine;

namespace Brodee.Core.Handlers
{
    public class BrodeeOptionsMenuHandler : Handler
    {
        private readonly IOptionMenuControls _optionMenuControls;
        private readonly GameObjectRepo _gameObjectRepo;

        public BrodeeOptionsMenuHandler(IOptionMenuControls optionMenuControls, GameObjectRepo gameObjectRepo)
        {
            _optionMenuControls = optionMenuControls;
            _gameObjectRepo = gameObjectRepo;
        }

        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            GameMenu.Get().ShowOptionsMenu();
            GameObject settingsWindow;
            if (!_gameObjectRepo.TryGet("BrodeeOptionsBareSettingsWindow", out settingsWindow))
            {
                settingsWindow = _optionMenuControls.CreateBareSettingWindow();
            }
            settingsWindow.SetActive(true);

            GameObject sliderCopy;
            if (!_gameObjectRepo.TryGet("BrodeeOptionsScrollbar", out sliderCopy))
            {
                sliderCopy = _optionMenuControls.CreateSliderCopy();
                var scrollbarControl = sliderCopy.GetComponent<ScrollbarControl>();
                scrollbarControl.SetUpdateHandler(val =>
                {
                    Logger.AppendLine("Changing scrollbarControl copy");
                });

                var sliderCopyText = sliderCopy.GetChildObjectContainingName("MusicVolumeLabel").GetComponent<UberText>();
                sliderCopyText.Text = "Slider Copy Text";
                sliderCopyText.UpdateText();
            }

            OptionsMenu.Get().Hide(false);

        }
    }
}