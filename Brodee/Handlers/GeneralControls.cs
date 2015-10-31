namespace Brodee.Handlers
{
    public class GeneralControls
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