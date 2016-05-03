using Brodee.Components;
using Brodee.Controls;
using Brodee.Core.Handlers;
using Brodee.Modules;
using Brodee.Triggers;
using UnityEngine;

namespace Brodee
{
    public class Brodee : MonoBehaviour
    {
        private HandlerHub _handlerHub;
        private GameState _oldGameState = new GameState { Mode = Scene.Unknown };
        private GameState _newGameState = new GameState { Mode = Scene.Unknown };
        private readonly DeckTileHolder _tileHolder = new DeckTileHolder();
        private readonly ModuleManager _moduleModuleManager = new ModuleManager();
        private GameObjectRepo _gameObjectRepo;

        private void LateUpdate()
        {
            _oldGameState = _newGameState;
            _newGameState = new GameState();

            _handlerHub.ProcessActions(_oldGameState, _newGameState);

            if (Input.GetKeyDown(KeyCode.F12))
            {
                _handlerHub.AddTrigger(new F12PressedTrigger());
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                _handlerHub.AddTrigger(new SliderAttemptTrigger());
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                _handlerHub.AddTrigger(new AddSettingsButtonTrigger());
            }

            EditableInterface.ProgressFrame();

            if (gameObject != null)
            {
                GameObject go;
                if (!_gameObjectRepo.TryGet("BrodeeGameObject", out go))
                {
                    Logger.AppendLine("Added BrodeeGameObject");
                    _gameObjectRepo.AddOrUpdate("BrodeeGameObject", gameObject);
                }
            }
        }

        private void Start()
        {
            _handlerHub = new HandlerHub();
            _gameObjectRepo = new GameObjectRepo();
            var gameMenuControls = new GameMenuControls();
            var optionMenuControls = new OptionMenuControls();
            var generalControls = new GeneralControls();


            _moduleModuleManager.LoadCore(_gameObjectRepo, _handlerHub, gameMenuControls, optionMenuControls, generalControls, () => _oldGameState, () => _newGameState);
            _moduleModuleManager.LoadModules(_handlerHub);

            var startUpHandler = new StartUpHandler(generalControls);

            startUpHandler.SpecificHandle(_oldGameState, _newGameState);
        }

        private void Awake() => _oldGameState = new GameState();

        private void OnDestroy()
        {
            Logger.AppendLine("Brodee.OnDestroy()");
        }
    }
}

