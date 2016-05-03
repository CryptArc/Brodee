using System.Collections.Generic;
using System.Linq;
using Brodee.Components;
using Brodee.Controls;
using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Core.Handlers
{
    public class BrodeeOptionsMenuHandler : TriggerHandler<OpenBrodeeMenuTrigger>
    {
        private readonly IOptionMenuControls _optionMenuControls;
        private readonly GameObjectRepo _gameObjectRepo;

        private readonly List<GameObject> _cubes = new List<GameObject>();

        public BrodeeOptionsMenuHandler(IOptionMenuControls optionMenuControls, GameObjectRepo gameObjectRepo)
        {
            _optionMenuControls = optionMenuControls;
            _gameObjectRepo = gameObjectRepo;
        }

        public override void SpecificHandle(OpenBrodeeMenuTrigger trigger, IGameState next)
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
                sliderCopy.transform.SetParent(settingsWindow.transform);
                var scrollbarControl = sliderCopy.GetComponent<ScrollbarControl>();
                scrollbarControl.SetUpdateHandler(val =>
                {
                    Logger.AppendLine("Changing scrollbarControl copy");
                });

                var sliderCopyText = sliderCopy.GetChildObjectContainingName("MusicVolumeLabel").GetComponent<UberText>();
                sliderCopyText.Text = "Slider Copy Text";
                sliderCopyText.UpdateText();
            }

            GameObject buttonCopy;
            if (!_gameObjectRepo.TryGet("BrodeeOptionsConsoleButton", out buttonCopy))
            {
                buttonCopy = _optionMenuControls.CreateButtonCopy();
                buttonCopy.transform.SetParent(settingsWindow.transform);
                var buttonControl = buttonCopy.GetComponent<UIBButton>();
                buttonControl.SetText("Console");
                buttonControl.AddEventListener(UIEventType.RELEASE, e =>
                {
                    //GameObject brodeeGameObject;
                    //if (_gameObjectRepo.TryGet("BrodeeGameObject", out brodeeGameObject))
                    //{
                    //    var goCamera = CameraUtils.FindFirstByLayer(brodeeGameObject.layer);
                    //    var basePos = CameraUtils.GetPosInFrontOfCamera(goCamera, goCamera.nearClipPlane);
                    //    var pos = new Vector3(basePos.x, basePos.y, basePos.z);
                    //    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //    cube.AddComponent<Rigidbody>();
                    //    cube.transform.position = pos;
                    //    cube.transform.SetParent(brodeeGameObject.transform, true);
                    //    //Helper.LogGameObjectComponents(cube);
                    //    _cubes.Add(cube);

                    //}
                    //Logger.AppendLine("Created Console cubes");
                });
            }
            OptionsMenu.Get().Hide(false);
            GameMenu.Get().Hide();
        }
    }
}