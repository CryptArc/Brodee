using Brodee.Components;
using UnityEngine;

namespace Brodee.HandlersDump
{
    public class PopUpBigTirionHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            var cardDef = DefLoader.Get().GetCardDef("EX1_383");
            var entDef = DefLoader.Get().GetEntityDef("EX1_383");
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
                CollectionDeckTray.Get().GetDeckBigCard().Show(entDef, TAG_PREMIUM.GOLDEN, cardDef, sourcePos, false);
            }
            
        }
    }
}