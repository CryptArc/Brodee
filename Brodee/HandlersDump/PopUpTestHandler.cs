using Brodee.Components;
using UnityEngine;

namespace Brodee.HandlersDump
{
    public class PopUpTestHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            //var goCamera = CameraUtils.FindFirstByLayer(Parent.gameObject.layer);
            //var pos = CameraUtils.GetPosInFrontOfCamera(goCamera, goCamera.nearClipPlane);
            //pos.y -= 50;
            //var notification = NotificationManager.Get().CreatePopupText(pos, 15f * Vector3.one, "Camera - 50");
            //NotificationManager.Get().DestroyNotification(notification, 2.5f);
        }
    }
}