using Brodee.Components;
using Brodee.HandlersDump;
using UnityEngine;

namespace Brodee.Core.Handlers
{
    public class CardCollectionGemColourChangeHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
            var ownedCardStacks = CollectionManager.Get().GetOwnedCards();

            foreach (var collectibleCard in ownedCardStacks)
            {
                var cardVisual = CollectionManagerDisplay.Get().m_pageManager.GetCardVisual(collectibleCard.CardId, collectibleCard.PremiumType);
                //Logger.AppendLine($"CardID:{keyValuePair.Value.CardID} OnPage:{cardVisual != null}");
                if (cardVisual != null)
                {
                    cardVisual.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.SetColor("_TintColor", Color.green);
                }
            }

        }
    }
}