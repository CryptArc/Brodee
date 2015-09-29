using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{
    public class SettingsButtonHandler : Handler
    {
        private GameObject _meshCopy;
        private BnetBarMenuButton _buttonCopy;

        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            _meshCopy = GameUtils.Instantiate(BnetBar.Get().m_menuButtonMesh, Parent) as GameObject;
            _buttonCopy = GameUtils.Instantiate(BnetBar.Get().m_menuButton, Parent) as BnetBarMenuButton;
            Logger.AppendLine($"BnetBar.Get().m_menuButtonMesh pos:{BnetBar.Get().m_menuButtonMesh.transform.position}");
            Logger.AppendLine($"BnetBar.Get().m_menuButton pos:{BnetBar.Get().m_menuButton.transform.position}");
            Logger.AppendLine($"_meshCopy pos:{_meshCopy.transform.position}");
            Logger.AppendLine($"_buttonCopy pos:{_buttonCopy.transform.position}");
            return EmptyTriggers;
        }
    }
}