using UnityEngine;

namespace BrodeStone.Handlers
{
    public class CardTileAttemptHandler : IHandler
    {
        private float _scale = 0.5f;

        private static int _count = 0;

        public HandlerType GetHandlerType => HandlerType.CardTileAttempt;

        public void Handle(GameObject component, GameState previous, GameState next)
        {
            //var goCamera = CameraUtils.FindFirstByLayer(_parent.gameObject.layer);
            //var posInFrontOfCamera = CameraUtils.GetPosInFrontOfCamera(goCamera, goCamera.nearClipPlane);
            //posInFrontOfCamera.y -= 50;

            _scale += (_count * 0.05f);
            GameObject gameObject = new GameObject("MyDeckTileVisual" + _scale);

            gameObject.transform.localScale = new Vector3(_scale, _scale, _scale);
            var pos = new Vector3((_count * 1f), 0, 0);
            gameObject.transform.localPosition = Vector3.zero;
            DeckTrayDeckTileVisual newTileVisual = gameObject.AddComponent<DeckTrayDeckTileVisual>();
            newTileVisual.gameObject.transform.localPosition = pos;
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
            _count++;

            gameObject.transform.SetParent(component.transform, true);
        }
    }
}