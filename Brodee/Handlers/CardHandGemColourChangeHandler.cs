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
                Logger.AppendLine("Attempting to set hand cards gem tint to green");
                foreach (var handCard in handCards)
                {
                    var color = handCard?.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.color;
                    if (handCard != null && color != null && color != Color.green)
                    {
                        Logger.AppendLine($"Attempting to hand card:{handCard.GetEntity().GetName()} gem tint to green");
                        handCard?.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.SetColor("_tint", Color.green);
                    }
                }
            }
            return EmptyTriggers;
        }
    }
}