using System;
using UnityEngine;

namespace Brodee
{
    public static class EditableInterface
    {
        private static GameObject _currentGameObject;
        private static Color _currentGameObjectOriginalColor;

        private static void ProgressFrameColor(int number)
        {
            if (_currentGameObject == null)
                return;

            var meshRenderer = _currentGameObject.GetComponent<MeshRenderer>();
            var first = Math.Sin(number / 30d) % 1.0d;
            var second = Math.Cos(number / 30d) % 1.0d;

            meshRenderer.material.color = new Color((float)first, (float)second, 0.5f);
        }

        public static void ProgressFrame()
        {
            if (_currentGameObject == null)
                return;
            ProgressFrameColor(Time.frameCount);
            if (Input.GetKeyDown(KeyCode.UpArrow))
                ShiftUp();
            if (Input.GetKeyDown(KeyCode.DownArrow))
                ShiftDown();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                ShiftLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow))
                ShiftRight();
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
                ScaleUp();
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
                ScaleDown();
        }

        public static void SetGameObject(GameObject go)
        {
            if (_currentGameObject != null)
            {
                _currentGameObject.GetComponent<MeshRenderer>().material.color = _currentGameObjectOriginalColor;
            }
            _currentGameObjectOriginalColor = go.GetComponent<MeshRenderer>().material.color;
            _currentGameObject = go;
        }

        private static void ShiftLeft(float x = 1.0f)
        {
            if (_currentGameObject == null)
                return;
            _currentGameObject.transform.localPosition = new Vector3(_currentGameObject.transform.localPosition.x - x, _currentGameObject.transform.localPosition.y, _currentGameObject.transform.localPosition.z);
            UiInterface.UpdateOrAdd(_currentGameObject.name,_currentGameObject);
        }

        private static void ShiftRight(float x = 1.0f)
        {
            if (_currentGameObject == null)
                return;
            _currentGameObject.transform.localPosition = new Vector3(_currentGameObject.transform.localPosition.x + x, _currentGameObject.transform.localPosition.y, _currentGameObject.transform.localPosition.z);
            UiInterface.UpdateOrAdd(_currentGameObject.name, _currentGameObject);
        }

        private static void ScaleUp(float amount = 0.1f)
        {
            if (_currentGameObject == null)
                return;
            var localScale = _currentGameObject.transform.localScale;
            _currentGameObject.transform.localScale = new Vector3(localScale.x * (1f + amount), localScale.y * (1f + amount), localScale.z * (1f + amount));
            Logger.AppendLine($"_currentGameObject.transform.pos:{_currentGameObject.transform.position}");
            Logger.AppendLine($"_currentGameObject.transform.localScale:{_currentGameObject.transform.localScale}");
            UiInterface.UpdateOrAdd(_currentGameObject.name, _currentGameObject);
        }

        private static void ScaleDown(float amount = 0.1f)
        {
            if (_currentGameObject == null)
                return;
            var localScale = _currentGameObject.transform.localScale;
            _currentGameObject.transform.localScale = new Vector3(localScale.x * (1f - amount), localScale.y * (1f - amount), localScale.z * (1f - amount));
            Logger.AppendLine($"_currentGameObject.transform.pos:{_currentGameObject.transform.position}");
            Logger.AppendLine($"_currentGameObject.transform.localScale:{localScale}");
            UiInterface.UpdateOrAdd(_currentGameObject.name, _currentGameObject);
        }

        private static void ShiftUp(float z = 1.0f)
        {
            if (_currentGameObject == null)
                return;
            _currentGameObject.transform.localPosition = new Vector3(_currentGameObject.transform.localPosition.x, _currentGameObject.transform.localPosition.y, _currentGameObject.transform.localPosition.z + z);
            UiInterface.UpdateOrAdd(_currentGameObject.name, _currentGameObject);
        }

        private static void ShiftDown(float z = 1.0f)
        {
            if (_currentGameObject == null)
                return;
            _currentGameObject.transform.localPosition = new Vector3(_currentGameObject.transform.localPosition.x, _currentGameObject.transform.localPosition.y, _currentGameObject.transform.localPosition.z - z);
            UiInterface.UpdateOrAdd(_currentGameObject.name, _currentGameObject);
        }
    }
}