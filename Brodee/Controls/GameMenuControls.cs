using UnityEngine;

namespace Brodee.Controls
{
    public interface IGameMenuControls
    {
        bool IsGameMenuOpen();
        bool IsButtonContainerAvailable();
        bool DoesGameMenuContainBrodeeSettingsAlready();
        void AddBrodeeSettingsButton(UIEvent.Handler onReleaseHandler);
        void LayoutGameMenuBackground();
        bool TryCreateMenuButton(string name, string text, UIEvent.Handler eventHandler, out UIBButton button);
    }

    public class GameMenuControls : IGameMenuControls
    {
        public bool IsGameMenuOpen()
        {
            if (GameMenu.Get() == null)
                return false;
            return GameMenu.Get().IsInGameMenu();
        }

        public bool IsButtonContainerAvailable()
        {
            var buttonListMenuDef = GameMenu.Get()
                ?.gameObject
                ?.GetComponent<GameMenu>()
                ?.gameObject
                ?.GetChildObjectContainingName("MenuBone")
                ?.GetChildObjectContainingName("ButtonListMenuDef")
                ?.GetComponent<ButtonListMenuDef>();
            return buttonListMenuDef != null;
        }

        private ButtonListMenuDef GetButtonListMenuDef()
        {
            return GameMenu.Get()
                ?.gameObject
                ?.GetComponent<GameMenu>()
                ?.gameObject
                ?.GetChildObjectContainingName("MenuBone")
                ?.GetChildObjectContainingName("ButtonListMenuDef")
                ?.GetComponent<ButtonListMenuDef>();
        }

        public bool DoesGameMenuContainBrodeeSettingsAlready()
        {
            if (!IsButtonContainerAvailable())
                return true;

            var buttonListMenuDef = GetButtonListMenuDef();
            return buttonListMenuDef.m_buttonContainer.m_slices[0].m_slice.name == GlobalStrings.BrodeeSettingsButton;
        }

        public void AddBrodeeSettingsButton(UIEvent.Handler onReleaseHandler)
        {
            if (!IsButtonContainerAvailable())
                return;
            var buttonListMenuDef = GetButtonListMenuDef();
            var slices = buttonListMenuDef.m_buttonContainer.m_slices.ToArray();
            var button = GameMenu.Get().CreateMenuButton(GlobalStrings.BrodeeSettingsButton, "Brodee", onReleaseHandler);
            button.gameObject.SetActive(true);
            buttonListMenuDef.m_buttonContainer.ClearSlices();
            buttonListMenuDef.m_buttonContainer.AddSlice(button.gameObject, Vector3.zero, Vector3.zero);
            foreach (var slice in slices)
            {
                buttonListMenuDef.m_buttonContainer.AddSlice(slice.m_slice, slice.m_minLocalPadding, slice.m_maxLocalPadding, slice.m_reverse);
            }
            buttonListMenuDef.m_buttonContainer.UpdateSlices();
        }

        public void LayoutGameMenuBackground()
        {
            if (!IsButtonContainerAvailable())
                return;
            var buttonListMenuDef = GetButtonListMenuDef();
            OrientedBounds orientedWorldBounds = TransformUtil.ComputeOrientedWorldBounds(buttonListMenuDef.m_buttonContainer.gameObject, true);
            float width = orientedWorldBounds.Extents[0].magnitude * 2f;
            float height = orientedWorldBounds.Extents[2].magnitude * 2f;
            buttonListMenuDef.m_background.SetSize(width, height);
            buttonListMenuDef.m_border.SetSize(width, height);
        }

        public bool TryCreateMenuButton(string name, string text, UIEvent.Handler eventHandler, out UIBButton button)
        {
            if (GameMenu.Get() != null)
            {
                button = GameMenu.Get().CreateMenuButton(name, text, eventHandler);
                button.gameObject.SetActive(true);
                return true;
            }
            button = null;
            return false;
        }
    }
}