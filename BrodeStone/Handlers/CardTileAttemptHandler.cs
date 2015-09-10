 using UnityEngine;

namespace BrodeStone.Handlers
{
    public class CardTileAttemptHandler : IHandler
    {
        private float _scale = 0.01f;
        
        private GameObject _gameObjectTile;

        public HandlerType GetHandlerType => HandlerType.CardTileAttempt;

        public void Handle(GameObject component, GameState previous, GameState next)
        {
            if (_gameObjectTile == null)
            {
                var goCamera = CameraUtils.FindFirstByLayer(component.gameObject.layer);
                var posInFrontOfCamera = CameraUtils.GetPosInFrontOfCamera(goCamera, goCamera.nearClipPlane);
                posInFrontOfCamera.y -= 1;

                _gameObjectTile = new GameObject("MyDeckTileVisual");

                _gameObjectTile.transform.localScale = new Vector3(_scale, _scale, _scale);
                _gameObjectTile.transform.localPosition = posInFrontOfCamera;
                DeckTrayDeckTileVisual newTileVisual = _gameObjectTile.AddComponent<DeckTrayDeckTileVisual>();
                newTileVisual.MarkAsUsed();
                newTileVisual.Show();
                CollectionDeckSlot deckSlot = new CollectionDeckSlot
                {
                    CardID = "EX1_383",
                    Count = 1,
                    Index = 0,
                    OnSlotEmptied = slot => { },
                    Premium = CardFlair.PremiumType.GOLDEN
                };
                newTileVisual.SetSlot(deckSlot, false);

                _gameObjectTile.transform.SetParent(component.transform, true);
            }
            else
            {
                var goCamera = CameraUtils.FindFirstByLayer(component.gameObject.layer);
                var mousePosition = Input.mousePosition;
                mousePosition.z = goCamera.nearClipPlane + 1.0f;
                var mouseWorldPos = goCamera.ScreenToWorldPoint(mousePosition);
                var gotT = _gameObjectTile.transform;
                gotT.localPosition = mouseWorldPos;
                Logger.AppendLine($"CardTileAttemptHandler position x:{gotT.position.x}, y:{gotT.position.y}, z:{gotT.position.z}");
                Logger.AppendLine($"CardTileAttemptHandler localPosition x:{gotT.localPosition.x}, y:{gotT.localPosition.y}, z:{gotT.localPosition.z}");
            }
        }
    }
}