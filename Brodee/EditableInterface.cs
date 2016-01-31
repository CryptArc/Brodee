using System;
using System.Collections.Generic;
using Brodee.Handlers;
using UnityEngine;

namespace Brodee
{
    public static class EditableInterface
    {
        private static int _currentGameObjectIndex;
        private static Color _currentGameObjectOriginalColor;

        private static List<GameObject> _registeredObjects = new List<GameObject>();

        private static void ProgressFrameColor(int number)
        {
            try
            {
                if (_registeredObjects.Count == 0)
                    return;

                var removedCount = 0;
                var startingCount = _registeredObjects.Count;

                for (int i = 0; i < startingCount; i++)
                {
                    if (_registeredObjects[i] == null)
                        _registeredObjects.RemoveAt(i);
                    startingCount--;
                    removedCount--;
                    i--;
                }

                if (removedCount > 0)
                {
                    _currentGameObjectIndex -= removedCount - 1;
                }
                if (_registeredObjects.Count == 0)
                    return;

                var meshRenderer = _registeredObjects[_currentGameObjectIndex].GetComponent<MeshRenderer>();
                var first = Math.Sin(number / 30d) % 1.0d;
                var second = Math.Cos(number / 30d) % 1.0d;

                meshRenderer.material.color = new Color((float)first, (float)second, 0.5f);
            }
            catch (Exception e)
            {
                Logger.AppendLine($"ProgressFrameColor error:{e}");
            }
        }

        public static void Register(GameObject go)
        {
            if (!_registeredObjects.Contains(go))
            {
                if (go.GetComponent<MeshRenderer>() == null)
                {
                    Logger.AppendLine($"EditableInterface.Register: Couldnt register {go.name} because it had no MeshRenderer");
                    return;
                }
                if (_registeredObjects.Count == 0)
                    _currentGameObjectOriginalColor = go.GetComponent<MeshRenderer>().material.color;
                _registeredObjects.Add(go);
                Logger.AppendLine($"EditableInterface.Register: Added {go.name}");
            }
        }

        private static void Next()
        {
            Logger.AppendLine($"EditableInterface.Next(): Before:_currentGameObjectIndex:{_currentGameObjectIndex}");
            var currentObj = GetGameObject();
            if (_registeredObjects.Count == 0)
            {
                Logger.AppendLine($"EditableInterface.Next(): We have no objects to Next to!");
                return;
            }
            currentObj.GetComponent<MeshRenderer>().material.color = _currentGameObjectOriginalColor;
            _currentGameObjectIndex = ++_currentGameObjectIndex % _registeredObjects.Count;
            Logger.AppendLine($"EditableInterface.Next(): After: _currentGameObjectIndex:{_currentGameObjectIndex}");
            currentObj = GetGameObject();
            _currentGameObjectOriginalColor = currentObj.GetComponent<MeshRenderer>().material.color;
        }

        private static GameObject GetGameObject()
        {
            if (_registeredObjects.Count == 0 || _registeredObjects[_currentGameObjectIndex] == null)
            {
                //Logger.AppendLine($"Getting gameObject was null: index:{_currentGameObjectIndex}");
                return null;
            }
            return _registeredObjects[_currentGameObjectIndex];
        }

        public static void ProgressFrame()
        {
            if (Input.GetKeyDown(KeyCode.PageDown))
                Next();
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

        private static void ShiftLeft(float x = 1.0f)
        {
            var gameObject = GetGameObject();
            if (gameObject == null)
                return;
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            UiInterface.UpdateOrAdd(gameObject.name, gameObject);
        }

        private static void ShiftRight(float x = 1.0f)
        {
            var gameObject = GetGameObject();
            if (gameObject == null)
                return;
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            UiInterface.UpdateOrAdd(gameObject.name, gameObject);
        }

        private static void ScaleUp(float amount = 0.1f)
        {
            var gameObject = GetGameObject();
            if (gameObject == null)
                return;
            var localScale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(localScale.x * (1f + amount), localScale.y * (1f + amount), localScale.z * (1f + amount));
            Logger.AppendLine($"_currentGameObject.transform.pos:{gameObject.transform.position}");
            Logger.AppendLine($"_currentGameObject.transform.localScale:{gameObject.transform.localScale}");
            UiInterface.UpdateOrAdd(gameObject.name, gameObject);
        }

        private static void ScaleDown(float amount = 0.1f)
        {
            var gameObject = GetGameObject();
            if (gameObject == null)
                return;
            var localScale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(localScale.x * (1f - amount), localScale.y * (1f - amount), localScale.z * (1f - amount));
            Logger.AppendLine($"_currentGameObject.transform.pos:{gameObject.transform.position}");
            Logger.AppendLine($"_currentGameObject.transform.localScale:{localScale}");
            UiInterface.UpdateOrAdd(gameObject.name, gameObject);
        }

        private static void ShiftUp(float z = 1.0f)
        {
            var gameObject = GetGameObject();
            if (gameObject == null)
                return;
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z + z);
            UiInterface.UpdateOrAdd(gameObject.name, gameObject);
        }

        private static void ShiftDown(float z = 1.0f)
        {
            var gameObject = GetGameObject();
            if (gameObject == null)
                return;
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z - z);
            UiInterface.UpdateOrAdd(gameObject.name, gameObject);
        }
    }
}