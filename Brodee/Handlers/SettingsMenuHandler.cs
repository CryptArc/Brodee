using Brodee.Components;
using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{
    public class SettingsMenuHandler : Handler
    {
        private GameObject _settingsMenuGameObject;
        private SettingsMenu _settingsMenu;

        public override void Setup(GameObject parent)
        {
            Logger.AppendLine("SettingsMenuHandler Setup");
            CreateSettingsMenu(parent);
            base.Setup(parent);
        }

        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            var gameMenuIsHidden = GameMenu.Get() == null || !GameMenu.Get().IsShown();
            var shouldHide = _settingsMenu.IsShown() &&
                            ((next.KeysPressed.Contains(KeyCode.F11) ||
                            !gameMenuIsHidden) ||
                            SceneMgr.Get().IsTransitioning());

            var shouldShow = !shouldHide &&
                            !SceneMgr.Get().IsTransitioning() &&
                            next.KeysPressed.Contains(KeyCode.F11) &&
                            gameMenuIsHidden;

            if (shouldHide)
            {
                Logger.AppendLine("SettingsMenuHandler Hide");
                _settingsMenu.Hide();
            }
            else if (shouldShow)
            {
                Logger.AppendLine("SettingsMenuHandler Show");
                _settingsMenu.Show();
            }
            return EmptyTriggers;
        }

        private void CreateSettingsMenu(GameObject parent)
        {
            if(parent == null)
                Logger.AppendLine("SettingsMenuHandler parent is null");
            _settingsMenuGameObject = new GameObject();
            _settingsMenuGameObject.transform.SetParent(parent.transform);
            _settingsMenu = _settingsMenuGameObject.AddComponent<SettingsMenu>();
        }
    }
}