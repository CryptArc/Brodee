using System.Collections.Generic;
using UnityEngine;

namespace Brodee.Components
{
    public class TileWrapper
    {
        public DeckTrayDeckTileVisual Tile { get; set; }
        public int Count { get; set; }
    }

    public class DeckTileHolder
    {
        private const float Scale = 0.01f;
        private const float ActualScale = 0.75f;
        private const float WidthAtScale = 150.0f; // At 0.01f scale

        private readonly TileSorter _tileSorter = new TileSorter();
        private readonly List<TileWrapper> _tiles = new List<TileWrapper>();
        private readonly Dictionary<string, TileWrapper> _tileDict = new Dictionary<string, TileWrapper>();

        private GameObject _holderGameObject;

        public void Update(GameObject component)
        {
            if (_holderGameObject == null)
            {
                Logger.AppendLine("DeckTileHolder.Update - _holderGameObject is null");
                _holderGameObject = new GameObject();
                _holderGameObject.transform.localScale = Vector3.one;
                var goCamera = CameraUtils.FindFirstByLayer(component.gameObject.layer);
                var mousePosition = Input.mousePosition;
                mousePosition.z = goCamera.nearClipPlane + 1.0f;
                mousePosition.x = WidthAtScale;
                var mouseWorldPos = goCamera.ScreenToWorldPoint(mousePosition);
                _holderGameObject.transform.localPosition = mouseWorldPos;
                _holderGameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

                _tileDict.Clear();
                while (_tiles.Count > 0)
                {
                    var index = _tiles.Count - 1;
                    var tile = _tiles[index];
                    Object.Destroy(tile.Tile.gameObject);
                    _tiles.RemoveAt(index);
                }
            }
            else
            {
                var goCamera = CameraUtils.FindFirstByLayer(component.gameObject.layer);
                var mousePosition = Input.mousePosition;
                mousePosition.z = goCamera.nearClipPlane + 1.0f;
                mousePosition.x = WidthAtScale;
                var mouseWorldPos = goCamera.ScreenToWorldPoint(mousePosition);
                _holderGameObject.transform.localPosition = mouseWorldPos;
            }
        }

        public void AddCard(string cardId)
        {
            var entityDef = DefLoader.Get().GetEntityDef(cardId);
            if (entityDef == null)
            {
                Logger.AppendLine($"DeckTileHolder.Update - entityDef is null for cardId:{cardId}");
                return;
            }
            TileWrapper tile;
            if (_tileDict.TryGetValue(cardId, out tile))
            {
                Logger.AppendLine($"DeckTileHolder.Update - Already have '{cardId}', increasing count");
                tile.Count++;
                CollectionDeckSlot deckSlot = new CollectionDeckSlot
                {
                    CardID = cardId,
                    Count = tile.Count,
                    Index = 0,
                    OnSlotEmptied = slot => { },
                    Premium = CardFlair.DEFAULT_PREMIUM_TYPE
                };
                tile.Tile.SetSlot(deckSlot, false);
            }
            else // Brand new card
            {
                Logger.AppendLine($"DeckTileHolder.Update - Dont have '{cardId}' so creating");
                var tileWrapper = CreateTileWrapper(cardId, 1, _tileDict.Count);
                _tiles.Add(tileWrapper);
                _tileDict.Add(cardId, tileWrapper);
            }
            var pos = _holderGameObject.transform.position;
            Logger.AppendLine($"_holderGameObject x:{pos.x} y:{pos.y} z:{pos.z}");
        }

        private TileWrapper CreateTileWrapper(string cardId, int count, int position)
        {
            var newTileObj = TileWrapperFactory.CreateTileWrapper(cardId, count, _holderGameObject,position);
            return newTileObj;
        }
    }
}