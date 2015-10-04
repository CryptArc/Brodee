using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{


    public class CheckBoxAttemptHandler : Handler
    {
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            Logger.AppendLine("CheckBoxAttemptHandler start");
            var checkBoxOrig = OptionsMenu.Get()?.m_fullScreenCheckbox;

            if (checkBoxOrig == null)
            {
                Logger.AppendLine("checkBoxOrig was null");
                return EmptyTriggers;
            }

            var checkBoxOrigGameObject = checkBoxOrig.gameObject;
            Helper.LogGameObjectComponents(checkBoxOrigGameObject);

            Logger.AppendLine($"checkboxPos:{checkBoxOrigGameObject.transform.position}");
            Logger.AppendLine($"checkboxLocalPos:{checkBoxOrigGameObject.transform.localPosition}");

            var copy = GameObject.Instantiate(checkBoxOrigGameObject);

            var goCamera = CameraUtils.FindFirstByLayer(BaseUI.Get().gameObject.layer);
            var mousePosition = Input.mousePosition;
            mousePosition.z = goCamera.nearClipPlane + 1.0f;
            var mouseWorldPos = goCamera.ScreenToWorldPoint(mousePosition);

            copy.transform.localPosition = checkBoxOrigGameObject.transform.position;

            return EmptyTriggers;
        }
    }
}