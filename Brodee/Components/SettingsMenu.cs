using System.Collections.Generic;

namespace Brodee.Components
{
    public class SettingsMenu : ButtonListMenu
    {
        private UIBButton _concedeButton;
        private UIBButton _resumeButton;

        protected override void Awake()
        {
            Logger.AppendLine("SettingsMenu Awake");
            base.Awake();
            _concedeButton = CreateMenuButton("ConcedeButton", "GLOBAL_CONCEDE", ConcedeButtonPressed);
            _resumeButton = CreateMenuButton("ResumeButton", "GLOBAL_RESUME_GAME", ResumeButtonPressed);
            m_menu.m_headerText.Text = "Brodee Menu";
            Logger.AppendLine("SettingsMenu Awake Finish");
        }

        private void ConcedeButtonPressed(UIEvent e)
        {
            Logger.AppendLine("SettingsMenu Concede button pressed.");
        }

        protected override void OnDestroy()
        {
            Logger.AppendLine("SettingsMenu Destroy");
            base.OnDestroy();
        }

        private void Start()
        {
            Logger.AppendLine("SettingsMenu Start");
            gameObject.SetActive(false);
        }

        protected override List<UIBButton> GetButtons()
        {
            Logger.AppendLine("SettingsMenu GetButtons");
            List<UIBButton> list = new List<UIBButton>();
            list.Add(_concedeButton);
            list.Add(null);
            list.Add(_resumeButton);
            return list;
        }
        
        protected override void LayoutMenu()
        {
            Logger.AppendLine("SettingsMenu LayoutMenu");
            LayoutMenuButtons();
            m_menu.m_buttonContainer.UpdateSlices();
            if (_concedeButton != null)
                _concedeButton.SetText(GameStrings.Get(!GameMgr.Get().IsSpectator() ? "GLOBAL_CONCEDE" : "GLOBAL_LEAVE_SPECTATOR_MODE"));
            LayoutMenuBackground();
        }

        private void ResumeButtonPressed(UIEvent e)
        {
            Logger.AppendLine("SettingsMenu Resume button pressed.");
            Hide();
        }
    }
}