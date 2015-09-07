using UnityEngine;

namespace BrodeStone.Handlers
{
    public class PopUpBigTirionHandler : IHandler
    {
        public HandlerType GetHandlerType => HandlerType.PopUpBigTirion;

        public void Handle(GameObject component, GameState previous, GameState next)
        {
            var cardDef = DefLoader.Get().GetCardDef("EX1_383");
            var entDef = DefLoader.Get().GetEntityDef("EX1_383");
            var cardFlair = new CardFlair(CardFlair.PremiumType.GOLDEN);
            var sourcePos = new Vector3(0, 0, 0);
            if (cardDef == null || entDef == null)
            {
                AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
                popupInfo.m_headerText = GameStrings.Get("Error");
                popupInfo.m_text = "Tirion CardDef or EntityDef havent been loaded yet";
                popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM;
                DialogManager.Get().ShowPopup(popupInfo, null, null);
            }
            else
            {
                CollectionDeckTray.Get().GetDeckBigCard().Show(entDef, cardFlair, cardDef, sourcePos);
            }
        }
    }
}