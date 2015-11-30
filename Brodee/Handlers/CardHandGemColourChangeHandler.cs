using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{
    public class CardHandGemColourChangeHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            if (global::GameState.Get() != null)
            {
                var handCards = global::GameState.Get().GetCurrentPlayer().GetHandZone().GetCards();
                foreach (var handCard in handCards)
                {
                    handCard?.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.SetColor("_TintColor", Color.green);
                }

                var deckCards = global::GameState.Get().GetCurrentPlayer().GetDeckZone().GetCards();
                foreach (var deckCard in deckCards)
                {
                    deckCard?.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.SetColor("_TintColor", Color.green);
                }

                var battlefieldCards = global::GameState.Get().GetCurrentPlayer().GetBattlefieldZone().GetCards();
                foreach (var bfcard in battlefieldCards)
                {
                    bfcard?.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.SetColor("_TintColor", Color.green);
                }
            }
        }
    }
}