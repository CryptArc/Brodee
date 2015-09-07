using System;
using UnityEngine;

namespace BrodeStone.Handlers
{
    public class CreateFlyingCubeHandler : IHandler
    {
        public HandlerType GetHandlerType => HandlerType.CreateFlyingCube;

        public void Handle(GameObject component, GameState previous, GameState next)
        {
            try
            {
                //gameObject = CreateObjectBasedOnPrefab<Notification>(NotificationManager.Get().popupTextPrefab);
                CreateFlyingCube(component);
            }
            catch (Exception e)
            {
                AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
                popupInfo.m_headerText = GameStrings.Get("Error");
                popupInfo.m_text = e.ToString();
                popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
                DialogManager.Get().ShowPopup(popupInfo, null, null);

            }
        }

        public GameObject CreateFlyingCube(GameObject gameObject)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<Rigidbody>();
            Logger.AppendLine(string.Format("CreateFlyingCube() gameObject.layer:{0}", gameObject.layer));
            var goCamera = CameraUtils.FindFirstByLayer(gameObject.layer);
            var pos = CameraUtils.GetPosInFrontOfCamera(goCamera, goCamera.nearClipPlane);
            pos.y -= 50;
            cube.transform.position = pos;
            cube.transform.SetParent(gameObject.transform, true);
            return cube;
        }
    }
}