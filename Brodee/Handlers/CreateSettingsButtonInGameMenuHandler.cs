using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{
    public class CreateSettingsButtonInGameMenuHandler : Handler
    {
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            var buttonListMenuDef = GameMenu.Get()?.gameObject?.GetChildObjectContainingName("ButtonListMenuDef")?.GetComponent<ButtonListMenuDef>();
            if (buttonListMenuDef == null)
            {
                Logger.AppendLine("CreateSettingsButtonInGameMenuHandler - buttonListMenuDef == null");
                return EmptyTriggers;
            }


            // LayoutMenuButtons();
            var slices = buttonListMenuDef.m_buttonContainer.m_slices.ToArray();
            Logger.AppendLine("Attempting to create button");
            var button = GameMenu.Get().CreateMenuButton("BrodeeSettings", "Brodee", @event => { });
            button.gameObject.SetActive(true);
            Logger.AppendLine("Clearing slices");
            buttonListMenuDef.m_buttonContainer.ClearSlices();
            Logger.AppendLine("Adding my slice");
            buttonListMenuDef.m_buttonContainer.AddSlice(button.gameObject, Vector3.zero, Vector3.zero, false);
            Logger.AppendLine("Adding old slices");
            foreach (var slice in slices)
            {
                buttonListMenuDef.m_buttonContainer.AddSlice(slice.m_slice, slice.m_minLocalPadding, slice.m_maxLocalPadding, slice.m_reverse);
            }
            Logger.AppendLine("Adding update slices");
            buttonListMenuDef.m_buttonContainer.UpdateSlices();

            //LayoutMenuBackground()
            OrientedBounds orientedWorldBounds = TransformUtil.ComputeOrientedWorldBounds(buttonListMenuDef.m_buttonContainer.gameObject, true);
            float width = orientedWorldBounds.Extents[0].magnitude * 2f;
            float height = orientedWorldBounds.Extents[2].magnitude * 2f;
            buttonListMenuDef.m_background.SetSize(width, height);
            buttonListMenuDef.m_border.SetSize(width, height);

            return EmptyTriggers;
        }
    }
}