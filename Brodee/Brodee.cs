using Brodee.Components;
using Brodee.Handlers;
using Brodee.Triggers;
using UnityEngine;

namespace Brodee
{
    public class Brodee : MonoBehaviour
    {
        private HandlerHub _handlerHub;

        private GameState _gameState;

        private readonly DeckTileHolder _tileHolder = new DeckTileHolder();

        private void LateUpdate()
        {
            var newSceneMode = SceneMgr.Get().GetMode().MapToScene();
            if (_gameState.Mode != newSceneMode)
                Logger.AppendLine($"Changing scene to {newSceneMode}");

            var newGameState = new GameState
            {
                Mode = newSceneMode
            };

            _handlerHub.ProcessActions(_gameState, newGameState);

            _gameState = newGameState;

            if (Input.GetKeyDown(KeyCode.F5))
            {
                _handlerHub.AddTrigger(new OpenSettingsMenuTrigger());
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                _handlerHub.AddTrigger(new SliderAttemptTrigger());
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                _handlerHub.AddTrigger(new AddSettingsButtonTrigger());
            }


            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                EditableInterface.ShiftUp();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                EditableInterface.ShiftDown();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                EditableInterface.ShiftLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                EditableInterface.ShiftRight();
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                EditableInterface.ScaleUp();
            }
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                EditableInterface.ScaleDown();
            }

            EditableInterface.ProgressFrameColor(Time.frameCount);
        }

        private void Start()
        {
            _gameState.GeneralControls.MakeConfirmPopUp("Start Up", "Just some text when starting up!");

            _handlerHub = new HandlerHub(gameObject);
            _handlerHub.RegisterOnTrigger<OpenSettingsMenuTrigger>(new BrodeeOptionsMenuHandler(), Scene.Hub);
            _handlerHub.RegisterOnTrigger<SliderAttemptTrigger>(new CardTileAttemptHandler(), Scene.Hub);
            _handlerHub.RegisterOnTrigger<AddSettingsButtonTrigger>(new CreateSettingsButtonInGameMenuHandler(), Scene.Hub);
            _handlerHub.Register(new CardHandGemColourChangeHandler(), HowOftenToProcess.EverySecond, Scene.GamePlay);
            _handlerHub.Register(new CardCollectionGemColourChangeHandler(), HowOftenToProcess.EverySecond, Scene.Collection);


            _gameState = new GameState
            {
                Mode = Scene.Unknown
            };
        }

        private void Awake() => _gameState = new GameState();
        private void OnDestroy() => Logger.AppendLine("Brodee.OnDestroy()");
    }
}

