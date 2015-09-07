using UnityEngine;

namespace BrodeStone.Handlers
{
    public class PopUpTestHandler : IHandler
    {
        public HandlerType GetHandlerType => HandlerType.PopUpTest;

        public void Handle(GameObject component, GameState previous, GameState next)
        {
            var goCamera = CameraUtils.FindFirstByLayer(component.gameObject.layer);
            var pos = CameraUtils.GetPosInFrontOfCamera(goCamera, goCamera.nearClipPlane);
            pos.y -= 50;
            var notification = NotificationManager.Get().CreatePopupText(pos, "Camera - 50");
            NotificationManager.Get().DestroyNotification(notification, 2.5f);

        }
    }
}