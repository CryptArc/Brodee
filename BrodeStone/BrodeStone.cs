using System.Collections.Generic;
using BrodeStone.Handlers;
using BrodeStone.Triggers;
using UnityEngine;

namespace BrodeStone
{
    public class BrodeStone : MonoBehaviour
    {
        private HandlerHub _handlerHub;
        public GameObject Obj;

        private GameState _gameState = new GameState();

        private readonly DeckTileHolder _tileHolder = new DeckTileHolder();

        private void LateUpdate()
        {
            var newGameState = new GameState
            {
                Mode = SceneMgr.Get().GetMode(),
                Cubes = _gameState.Cubes ?? new List<GameObject>()
            };
            KeyPressedHelper.PopulateGameState(newGameState);

            if (Obj == null)
            {
                Obj = new GameObject();
                Obj.transform.SetParent(gameObject.transform);
            }

            if (Time.frameCount % 60 == 0)
            {
                _handlerHub.ProcessActions(_gameState, newGameState);
                if (global::GameState.Get() != null)
                {
                    var handCards = global::GameState.Get().GetCurrentPlayer().GetHandZone().GetCards();
                    Logger.AppendLine("Attempting to set hand cards gem tint to green");
                    foreach (var handCard in handCards)
                    {
                        Logger.AppendLine($"Attempting to hand card:{handCard.GetEntity().GetName()} gem tint to green");
                        var col = Color.green;
                        handCard?.GetActor()?.m_rarityGemMesh?.renderer?.material?.SetColor("_tint", col);
                    }
                }
            }

            _gameState = newGameState;
        }

        private void Start()
        {
            AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
            popupInfo.m_headerText = GameStrings.Get("StartUp");
            popupInfo.m_text = "Just some Text. Not so bad.";
            popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM;
            DialogManager.Get().ShowPopup(popupInfo, null, null);

            _handlerHub = new HandlerHub(gameObject);
            _handlerHub.RegisterOnTrigger<CreateFlyingCubesTrigger>(new CreateFlyingCubesHandler(), ScenesToProcessOn.All);
            _handlerHub.Register(new CreateFlyingCubesHandler(), HowOftenToProcess.EverySecond, ScenesToProcessOn.All);
        }

        private void Awake() => _gameState = new GameState();
        private void OnDestroy() => Logger.AppendLine("BrodeStone.OnDestroy()");
    }
}

