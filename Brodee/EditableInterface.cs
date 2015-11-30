using System;
using Brodee.Handlers;
using UnityEngine;

namespace Brodee
{
    public static class EditableInterface
    {
        private static GameObject _currentGameObject;

        public static void ProgressFrameColor(int number)
        {
            if (_currentGameObject == null)
                return;

            var meshRenderer = _currentGameObject.GetComponent<MeshRenderer>();
            var first = Math.Sin(number / 60d) % 1.0d;
            var second = Math.Cos(number / 60d) % 1.0d;

            meshRenderer.material.color = new Color((float)first, (float)second, 0.5f);
        }

        public static void SetGameObject(GameObject go)
        {
            _currentGameObject = go;
        }

        public static void ShiftLeft(float x = 1.0f)
        {
            if (_currentGameObject == null)
                return;
            _currentGameObject.transform.localPosition = new Vector3(_currentGameObject.transform.localPosition.x - x, _currentGameObject.transform.localPosition.y, _currentGameObject.transform.localPosition.z);
        }

        public static void ShiftRight(float x = 1.0f)
        {
            if (_currentGameObject == null)
                return;
            _currentGameObject.transform.localPosition = new Vector3(_currentGameObject.transform.localPosition.x + x, _currentGameObject.transform.localPosition.y, _currentGameObject.transform.localPosition.z);
        }

        public static void ScaleUp(float amount = 0.1f)
        {
            if (_currentGameObject == null)
                return;
            var localScale = _currentGameObject.transform.localScale;
            _currentGameObject.transform.localScale = new Vector3(localScale.x * (1f + amount), localScale.y * (1f + amount), localScale.z * (1f + amount));
            Logger.AppendLine($"_currentGameObject.transform.pos:{_currentGameObject.transform.position}");
            Logger.AppendLine($"_currentGameObject.transform.localScale:{_currentGameObject.transform.localScale}");
        }

        public static void ScaleDown(float amount = 0.1f)
        {
            if (_currentGameObject == null)
                return;
            var localScale = _currentGameObject.transform.localScale;
            _currentGameObject.transform.localScale = new Vector3(localScale.x * (1f - amount), localScale.y * (1f - amount), localScale.z * (1f - amount));
            Logger.AppendLine($"_currentGameObject.transform.pos:{_currentGameObject.transform.position}");
            Logger.AppendLine($"_currentGameObject.transform.localScale:{localScale}");
        }

        public static void ShiftUp(float z = 1.0f)
        {
            if (_currentGameObject == null)
                return;
            _currentGameObject.transform.localPosition = new Vector3(_currentGameObject.transform.localPosition.x, _currentGameObject.transform.localPosition.y, _currentGameObject.transform.localPosition.z + z);

        }

        public static void ShiftDown(float z = 1.0f)
        {
            if (_currentGameObject == null)
                return;
            _currentGameObject.transform.localPosition = new Vector3(_currentGameObject.transform.localPosition.x, _currentGameObject.transform.localPosition.y, _currentGameObject.transform.localPosition.z - z);

        }
    }
}