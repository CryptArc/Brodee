using Brodee.Triggers;

namespace Brodee.Handlers
{
    public class PopUpTestHandler : Handler
    {
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            var goCamera = CameraUtils.FindFirstByLayer(Parent.gameObject.layer);
            var pos = CameraUtils.GetPosInFrontOfCamera(goCamera, goCamera.nearClipPlane);
            pos.y -= 50;
            var notification = NotificationManager.Get().CreatePopupText(pos, "Camera - 50");
            NotificationManager.Get().DestroyNotification(notification, 2.5f);
            return EmptyTriggers;
        }
    }
}