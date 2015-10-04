using System.Collections.Generic;
using Brodee.Components;
using Brodee.Handlers;
using Brodee.Triggers;
using UnityEngine;

namespace Brodee
{
    public class Brodee : MonoBehaviour
    {
        private HandlerHub _handlerHub;
        public GameObject Obj;

        private GameState _gameState = new GameState();

        private readonly DeckTileHolder _tileHolder = new DeckTileHolder();

        private void LateUpdate()
        {
            var newGameState = new GameState
            {
                Mode = SceneMgr.Get().GetMode().MapToScene(),
                Cubes = _gameState.Cubes ?? new List<GameObject>()
            };
            KeyPressedHelper.PopulateGameState(newGameState);

            if (Obj == null)
            {
                Obj = new GameObject();
                Obj.transform.SetParent(gameObject.transform);
            }

            _handlerHub.ProcessActions(_gameState, newGameState);

            _gameState = newGameState;

            if (Input.GetKeyDown(KeyCode.F10))
            {
                _handlerHub.AddTrigger(new CardCollectionTrigger());
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                _handlerHub.AddTrigger(new CheckBoxAttemptTrigger());
            }
        }

        private void Start()
        {
            AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
            popupInfo.m_headerText = GameStrings.Get("StartUp");
            popupInfo.m_text = "Just some Text. Not so bad.";
            popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM;
            DialogManager.Get().ShowPopup(popupInfo, null, null);

            _handlerHub = new HandlerHub(gameObject);
            _handlerHub.RegisterOnTrigger<CreateFlyingCubesTrigger>(new CreateFlyingCubesHandler(), Handlers.Scene.All);
            _handlerHub.RegisterOnTrigger<CardCollectionTrigger>(new CardCollectionGemColourChangeHandler(), Handlers.Scene.Collection);
            //_handlerHub.RegisterOnTrigger<CheckBoxAttemptTrigger>(new CheckBoxAttemptHandler(), Handlers.Scene.All);
            _handlerHub.RegisterOnTrigger<CheckBoxAttemptTrigger>(new BrodeeOptionsMenuHandler(), Handlers.Scene.All);
            //_handlerHub.Register(new CreateFlyingCubesHandler(), HowOftenToProcess.EverySecond, Handlers.Scene.All);
            _handlerHub.Register(new CardHandGemColourChangeHandler(), HowOftenToProcess.EverySecond, Handlers.Scene.GamePlay);

            _gameState = new GameState
            {
                Mode = Handlers.Scene.Unknown,
                Cubes = new List<GameObject>()
            };
        }

        private void Awake() => _gameState = new GameState();
        private void OnDestroy() => Logger.AppendLine("Brodee.OnDestroy()");
    }
}

