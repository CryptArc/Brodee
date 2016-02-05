using Brodee.Controls;
using UnityEngine;

namespace Brodee
{
    public interface IGameState
    {
        Scene Mode { get; }

    }

    public class GameState : IGameState
    {
        public Scene Mode { get; set; } = Scene.None;

        private GameObject _uiCameraGameObject;

        public GameObject GetUiCameraGameObject()
        {
            if (_uiCameraGameObject == null)
            {
                var camera = Camera.main;
                var cameraPos = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, camera.nearClipPlane));
                _uiCameraGameObject = new GameObject("Brodee.UiCameraGameObject");
                _uiCameraGameObject.transform.localPosition = new Vector3(cameraPos.x, cameraPos.y, 200.0f);
            }
            return _uiCameraGameObject;
        }
    }
}