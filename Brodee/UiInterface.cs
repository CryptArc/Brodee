using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Brodee
{
    public class UiInterfaceObject
    {
        public string Name { get; }
        public float LocalPositionX { get; }
        public float LocalPositionY { get; }
        public float LocalPositionZ { get; }
        public float LocalScaleX { get; }
        public float LocalScaleY { get; }
        public float LocalScaleZ { get; }
        public float LocalRotationX { get; }
        public float LocalRotationY { get; }
        public float LocalRotationZ { get; }
        public float LocalRotationW { get; }
        public float RotationX { get; }
        public float RotationY { get; }
        public float RotationZ { get; }
        public float RotationW { get; }

        [JsonConstructor]
        public UiInterfaceObject(string name, float localPositionX, float localPositionY, float localPositionZ,
                                float localScaleX, float localScaleY, float localScaleZ,
                                float localRotationX, float localRotationY, float localRotationZ, float localRotationW,
                                float rotationX, float rotationY, float rotationZ, float rotationW)

        {
            Name = name;
            LocalPositionX = localPositionX;
            LocalPositionY = localPositionY;
            LocalPositionZ = localPositionZ;
            LocalScaleX = localScaleX;
            LocalScaleY = localScaleY;
            LocalScaleZ = localScaleZ;
            LocalRotationX = localRotationX;
            LocalRotationY = localRotationY;
            LocalRotationZ = localRotationZ;
            LocalRotationW = localRotationW;
            RotationX = rotationX;
            RotationY = rotationY;
            RotationZ = rotationZ;
            RotationW = rotationW;
        }

        public UiInterfaceObject(string name, Vector3 localPos, Vector3 localScale, Quaternion localRot, Quaternion rot)
        {
            Name = name;
            LocalPositionX = localPos.x;
            LocalPositionY = localPos.y;
            LocalPositionZ = localPos.z;
            LocalScaleX = localScale.x;
            LocalScaleY = localScale.y;
            LocalScaleZ = localScale.z;
            LocalRotationX = localRot.x;
            LocalRotationY = localRot.y;
            LocalRotationZ = localRot.z;
            LocalRotationW = localRot.w;
            RotationX = rot.x;
            RotationY = rot.y;
            RotationZ = rot.z;
            RotationW = rot.w;
        }

        public UiInterfaceObject(string name, GameObject gameObject)
        {
            Name = name;
            LocalPositionX = gameObject.transform.position.x;
            LocalPositionY = gameObject.transform.position.y;
            LocalPositionZ = gameObject.transform.position.z;
            LocalScaleX = gameObject.transform.localScale.x;
            LocalScaleY = gameObject.transform.localScale.y;
            LocalScaleZ = gameObject.transform.localScale.z;
            LocalRotationX = gameObject.transform.localRotation.x;
            LocalRotationY = gameObject.transform.localRotation.y;
            LocalRotationZ = gameObject.transform.localRotation.z;
            LocalRotationW = gameObject.transform.localRotation.w;
            RotationX = gameObject.transform.rotation.x;
            RotationY = gameObject.transform.rotation.y;
            RotationZ = gameObject.transform.rotation.z;
            RotationW = gameObject.transform.rotation.w;
        }
    }

    public static class UiInterface
    {
        private static readonly Dictionary<string, UiInterfaceObject> InterfaceObjects = new Dictionary<string, UiInterfaceObject>();

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };

        static UiInterface()
        {
            if (File.Exists("config.json"))
            {
                var json = File.ReadAllText("config.json");
                var array = JsonConvert.DeserializeObject<UiInterfaceObject[]>(json, JsonSerializerSettings);
                foreach (var uiInterfaceObject in array)
                {
                    InterfaceObjects.Add(uiInterfaceObject.Name, uiInterfaceObject);
                }
            }
        }

        static void WriteConfig()
        {
            var json = JsonConvert.SerializeObject(InterfaceObjects.Values.ToArray(), JsonSerializerSettings);
            File.WriteAllText("config.json", json);
        }

        public static void UpdateOrAdd(string name, GameObject newGameObject)
        {
            var newObject = new UiInterfaceObject(name, newGameObject);
            InterfaceObjects[name] = newObject;
            WriteConfig();
        }

        public static bool TryPopulateOrAdd(string name, GameObject newGameObject, GameObject oldGameObject)
        {
            UiInterfaceObject uiInterfaceObject;
            newGameObject.name = name;
            if (InterfaceObjects.TryGetValue(name, out uiInterfaceObject))
            {
                Logger.AppendLine($"UiInterfaceObject:{name}, recall positions, LocalPosX:{uiInterfaceObject.LocalPositionX} LocalPosY:{uiInterfaceObject.LocalPositionY} LocalPosZ:{uiInterfaceObject.LocalPositionZ}");
                newGameObject.transform.localPosition = new Vector3(uiInterfaceObject.LocalPositionX,
                    uiInterfaceObject.LocalPositionY, uiInterfaceObject.LocalPositionZ);
                newGameObject.transform.localScale = new Vector3(uiInterfaceObject.LocalScaleX,
                    uiInterfaceObject.LocalScaleY, uiInterfaceObject.LocalScaleZ);
                newGameObject.transform.localRotation = new Quaternion(uiInterfaceObject.LocalRotationX,
                    uiInterfaceObject.LocalRotationY, uiInterfaceObject.LocalRotationZ,
                    uiInterfaceObject.LocalRotationW);
                newGameObject.transform.rotation = new Quaternion(uiInterfaceObject.RotationX,
                    uiInterfaceObject.RotationY, uiInterfaceObject.RotationZ,
                    uiInterfaceObject.RotationW);
                return true;
            }
            Logger.AppendLine($"UiInterfaceObject:{name}, wasn't there adding new");
            var newObject = new UiInterfaceObject(name, oldGameObject);
            InterfaceObjects.Add(name, newObject);
            WriteConfig();
            return false;
        }
    }
}