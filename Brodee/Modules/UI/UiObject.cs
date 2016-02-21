using System.Net;
using Brodee.Controls;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Brodee.Modules.UI
{
    public class ObjectPosition
    {
        public static ObjectPosition GetDefault() => new ObjectPosition
        {
            LocalPositionX = 1.0f,
            LocalPositionY = 1.0f,
            LocalPositionZ = 1.0f,
            LocalScaleX = 1.0f,
            LocalScaleY = 1.0f,
            LocalScaleZ = 1.0f,
            LocalRotationX = 1.0f,
            LocalRotationY = 1.0f,
            LocalRotationZ = 1.0f,
            LocalRotationW = 1.0f,
            RotationX = 1.0f,
            RotationY = 1.0f,
            RotationZ = 1.0f,
            RotationW = 1.0f,
        };

        public float LocalPositionX;
        public float LocalPositionY;
        public float LocalPositionZ;
        public float LocalScaleX;
        public float LocalScaleY;
        public float LocalScaleZ;
        public float LocalRotationX;
        public float LocalRotationY;
        public float LocalRotationZ;
        public float LocalRotationW;
        public float RotationX;
        public float RotationY;
        public float RotationZ;
        public float RotationW;
    }

    public class UiObject
    {
        [JsonIgnore]
        public GameObject GameObject { private set; get; }
        [JsonIgnore]
        public bool DoesExist => GameObject != null;

        public readonly ObjectPosition Position;
        public readonly string Name;
        public readonly UiType UiType;

        [JsonConstructor]
        public UiObject(string name, UiType uiType, ObjectPosition position)
        {
            UiType = uiType;
            Name = name;
            Position = position;
        }

        
        public void TryLoad(OptionMenuControls optionMenuControls)
        {
            switch (UiType)
            {
                case UiType.BareSettingsWindow:
                    var bareSettingWindow = optionMenuControls.CreateBareSettingWindow();
                    GameObject = bareSettingWindow;
                    break;
                case UiType.Slider:
                    var sliderCopy = optionMenuControls.CreateSliderCopy();
                    GameObject = sliderCopy;
                    break;
                case UiType.Unknown:
                    Logger.AppendLine($"TryLoad: Unable to create type:{UiType} name:{Name}");
                    break;
            }
        }

        public void Destroy()
        {
            if (GameObject != null)
            {
                Object.Destroy(GameObject);
            }
        }
    }
}