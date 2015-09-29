using UnityEngine;

namespace BrodeStone
{
    public static class TileWrapperFactory
    {
        private const float Scale = 1.0f;
        private const float HeightDistance = 4.0f;

        public static TileWrapper CreateTileWrapper(string cardId, int count, GameObject holderGameObject, int position)
        {
            var newTileObj = new GameObject("MyDeckTileVisual");
            var newTileVisual = newTileObj.AddComponent<DeckTrayDeckTileVisual>();
            Logger.AppendLine("Creating card based on new instance");
            newTileVisual.MarkAsUsed();
            newTileVisual.Show();
            CollectionDeckSlot deckSlot = new CollectionDeckSlot
            {
                CardID = cardId,
                Count = count,
                Index = 0,
                OnSlotEmptied = slot => { },
                Premium = CardFlair.PremiumType.GOLDEN
            };
            newTileVisual.SetSlot(deckSlot, false);
            newTileObj.transform.SetParent(holderGameObject.transform);
            newTileObj.transform.localScale = new Vector3(Scale, Scale, Scale);
            newTileObj.transform.localPosition = new Vector3(0f, 0f, position * HeightDistance);
            var tileWrapper = new TileWrapper { Tile = newTileVisual, Count = count };
            
            return tileWrapper;
        }
    }
}