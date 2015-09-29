using System;
using System.Collections.Generic;
using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{
    public class CreateFlyingCubesHandler : Handler
    {
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            try
            {
                //gameObject = CreateObjectBasedOnPrefab<Notification>(NotificationManager.Get().popupTextPrefab);
                CreateFlyingCubes(Parent, next.Cubes);
            }
            catch (Exception e)
            {
                AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
                popupInfo.m_headerText = GameStrings.Get("Error");
                popupInfo.m_text = e.ToString();
                popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
                DialogManager.Get().ShowPopup(popupInfo, null, null);

            }
            return EmptyTriggers;
        }

        public void CreateFlyingCubes(GameObject gameObject, List<GameObject> list)
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
                    list.Add(cube);
                }
            }
            Logger.AppendLine($"CreateFlyingCubes() gameObject.layer:{gameObject.layer}");
        }
    }
}