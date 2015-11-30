namespace Brodee.Controls
{
    public interface IGeneralControls
    {
        void MakeConfirmPopUp(string title, string text);
    }

    public class GeneralControls : IGeneralControls
    {
        public void MakeConfirmPopUp(string title, string text)
        {
            AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
            popupInfo.m_headerText = title;
            popupInfo.m_text = text;
            popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM;
            DialogManager.Get().ShowPopup(popupInfo, null, null);
        }
    }
}