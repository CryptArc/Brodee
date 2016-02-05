using Brodee.Components;
using Brodee.HandlersDump;
using UnityEngine;

namespace Brodee.Core.Handlers
{
    public class CardCollectionGemColourChangeHandler : Handler
    {
        public override void SpecificHandle(IGameState previous, IGameState next)
        {
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
        }
    }
}