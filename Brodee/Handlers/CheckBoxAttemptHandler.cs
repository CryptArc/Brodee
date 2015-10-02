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

            Logger.AppendLine($"copy checkboxPos:{copy.transform.position}");
            Logger.AppendLine($"copy checkboxLocalPos:{copy.transform.localPosition}");

            OptionsMenu.Get()?.m_leftPane.ClearSlices();
            OptionsMenu.Get()?.m_leftPane.gameObject.SetActive(false);
            OptionsMenu.Get()?.m_rightPane.ClearSlices();
            OptionsMenu.Get()?.m_middlePane.ClearSlices();

            Helper.LogGameObjectComponents(OptionsMenu.Get().gameObject);

            return EmptyTriggers;
        }
    }
}