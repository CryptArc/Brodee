using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{
    public class CardHandGemColourChangeHandler : Handler
    {
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            if (global::GameState.Get() != null)
            {
                var handCards = global::GameState.Get().GetCurrentPlayer().GetHandZone().GetCards();
                Logger.AppendLine("Attempting to recolour gems");
                foreach (var handCard in handCards)
                {
                    if (handCard != null)
                    {
                        handCard.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.SetColor("_TintColor", Color.green);
                        //handCard.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.SetColor("_Color", Color.green);
                    }
                }

                var deckCards = global::GameState.Get().GetCurrentPlayer().GetDeckZone().GetCards();
                foreach (var deckCard in deckCards)
                {
                    if (deckCard != null)
                    {
                        deckCard.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.SetColor("_TintColor", Color.green);
                        //deckCard.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.SetColor("_Color", Color.green);
                    }
                }

                var battlefieldCards = global::GameState.Get().GetCurrentPlayer().GetBattlefieldZone().GetCards();
                foreach (var bfcard in battlefieldCards)
                {
                    if (bfcard != null)
                    {
                        bfcard.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.SetColor("_TintColor", Color.green);
                        //bfcard.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.SetColor("_Color", Color.green);
                    }
                }
            }
            return EmptyTriggers;
        }
    }
}