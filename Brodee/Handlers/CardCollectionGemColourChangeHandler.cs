using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{
    public class CardCollectionGemColourChangeHandler : Handler
    {
        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {

            Logger.AppendLine($"Attempting to check CardCollection page");
            var ownedCardStacks = CollectionManager.Get().GetOwnedCardStacks();
            foreach (var collectionCardStack in ownedCardStacks)
            {
                foreach (var keyValuePair in collectionCardStack.GetArtStacks())
                {
                    var cardVisual = CollectionManagerDisplay.Get().m_pageManager.GetCardVisual(keyValuePair.Value.CardID, keyValuePair.Value.Flair);
                    //Logger.AppendLine($"CardID:{keyValuePair.Value.CardID} OnPage:{cardVisual != null}");
                    if (cardVisual != null)
                    {
                        cardVisual.GetActor()?.m_rarityGemMesh?.GetComponent<Renderer>()?.material?.SetColor("_TintColor", Color.green);
                    }
                }
            }
            return EmptyTriggers;
        }
    }
}