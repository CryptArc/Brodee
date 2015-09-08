using System;
using UnityEngine;

namespace BrodeStone.Handlers
{
    public class CreateFlyingCubesHandler : IHandler
    {
        public HandlerType GetHandlerType => HandlerType.CreateFlyingCubes;

        public void Handle(GameObject component, GameState previous, GameState next)
        {
            try
            {
                //gameObject = CreateObjectBasedOnPrefab<Notification>(NotificationManager.Get().popupTextPrefab);
                CreateFlyingCubes(component);
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

        public void CreateFlyingCubes(GameObject gameObject)
        {

            var goCamera = CameraUtils.FindFirstByLayer(gameObject.layer);
            var basePos = CameraUtils.GetPosInFrontOfCamera(goCamera, goCamera.nearClipPlane);


            float startVal = 30.0f;
            float inc = startVal / 10.0f;
            for (float x = -startVal; x < startVal; x += inc)
            {
                for (float z = -startVal; z < startVal; z += inc)
                {
                    var pos = new Vector3(basePos.x, basePos.y, basePos.z);
                    pos.y -= 50;
                    pos.x += x;
                    pos.z += z;
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.AddComponent<Rigidbody>();
                    cube.transform.position = pos;
                    cube.transform.SetParent(gameObject.transform, true);
                }
            }
            Logger.AppendLine(string.Format("CreateFlyingCubes() gameObject.layer:{0}", gameObject.layer));
        }
    }
}