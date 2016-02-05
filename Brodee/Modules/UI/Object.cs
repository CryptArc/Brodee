using System;
using UnityEngine;
using Type = Brodee.Modules.UI.Type;

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

    public class Object
    {
        public readonly GameObject GameObject;
        public readonly ObjectPosition Position = ObjectPosition.GetDefault();
        public readonly string Name;
        public readonly Type Type;

        public bool DoesExist => GameObject != null;

        public ObjectPosition DefaultPosition;

        public Object(string name, Type type)
        {
            Type = type;
            Name = name;
        }

        public void TryLoad()
        {
            throw new NotImplementedException();
        }
    }
}