using System;
using System.Collections.Generic;
using System.IO;
using BrodeStone.Handlers;
using PegasusGame;
using UnityEngine;
// ReSharper disable UnusedMember.Local
// ReSharper disable InconsistentNaming

namespace BrodeStone
{
    public class BrodeStone : MonoBehaviour
    {
        private readonly string _logoutput = "MyFileOutput.txt";
        private readonly HandlerHub _handlerHub = new HandlerHub();
        public GameObject Obj;

        private GameState _gameState;

        private void Update()
        {
            var newGameState = new GameState
            {
                Mode = SceneMgr.Get().GetMode()
            };

            if (_gameState.Mode != newGameState.Mode)
            {
                Logger.AppendLine(string.Format("Time.framecount:{0}", Time.frameCount));
                Logger.AppendLine("Expect obj to be null...");
                if (Obj != null)
                    Logger.AppendLine("obj is not null");
            }

            if (Obj == null)
            {
                Logger.AppendLine(string.Format("obj is null, framecount:{0}", Time.frameCount));
                Obj = new GameObject();
                Obj.transform.SetParent(gameObject.transform);
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                _handlerHub.ProcessType(HandlerType.DestroyChildGameObjects);
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                _handlerHub.ProcessType(HandlerType.CardTileAttempt);
            }
            if (Input.GetKeyDown(KeyCode.F8))
            {
                _handlerHub.ProcessType(HandlerType.PopUpBigTirion);
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                _handlerHub.ProcessType(HandlerType.PopulatePaladinDeck);
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                _handlerHub.ProcessType(HandlerType.PopUpTest);
            }
            if (Input.GetKeyDown(KeyCode.F12))
            {
                _handlerHub.ProcessType(HandlerType.CreateFlyingCubes);
            }

            if (_handlerHub.HasActions())
            {
                Logger.AppendLine("_handlerHub.ProcessActions");
                _handlerHub.ProcessActions(Obj, _gameState, newGameState);
            }

            _gameState = newGameState;
        }

        private void Start()
        {
            Network.Get().RegisterNetHandler(PowerHistory.PacketID.ID, OnPowerHistory);
            //AdventureConfig.Get().ChangeSubScene(AdventureSubScenes.Practice);
            //SceneMgr.Get().SetNextMode(SceneMgr.Mode.ADVENTURE)

            AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
            popupInfo.m_headerText = GameStrings.Get("StartUp");
            popupInfo.m_text = "Just some Text. Not so bad.";
            popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM;
            DialogManager.Get().ShowPopup(popupInfo, null, null);
            _handlerHub.Register(HandlerType.CardTileAttempt, new CardTileAttemptHandler());
            _handlerHub.Register(HandlerType.PopulatePaladinDeck, new PopulatePaladinDeckHandler());
            _handlerHub.Register(HandlerType.CreateFlyingCubes, new CreateFlyingCubesHandler());
            _handlerHub.Register(HandlerType.DestroyChildGameObjects, new DestroyChildGameObjectsHandler());
            _handlerHub.Register(HandlerType.PopUpBigTirion, new PopUpBigTirionHandler());
            _handlerHub.Register(HandlerType.PopUpTest, new PopUpTestHandler());
        }

        private void Awake()
        {
            _gameState = new GameState();
        }

        private Network.Entity _fullent;

        private void OnPowerHistory()
        {
            try
            {
                ConnectAPI.GetPowerHistory().ForEach(history =>
                {
                    if (history.Type == Network.PowerType.FULL_ENTITY)
                    {
                        var defLoader = DefLoader.Get();
                        var fullEnt = (Network.HistFullEntity)history;
                        _fullent = fullEnt.Entity;
                        var strList = new List<string>();
                        fullEnt.Entity.Tags.ForEach(tag =>
                        {
                            strList.Add(tag.Name + "," + tag.Value);
                        });
                        var entDef = defLoader.GetEntityDef(fullEnt.Entity.CardID);

                        var str = string.Format("name={0} id={1} cardId={2} tags={3}", entDef.GetName(),
                            fullEnt.Entity.ID, fullEnt.Entity.CardID,
                            string.Join("|", strList.ToArray()));

                        File.AppendAllText(_logoutput, str);
                        File.AppendAllText(_logoutput, Environment.NewLine);
                    }
                    else if (history.Type == Network.PowerType.SHOW_ENTITY)
                    {
                        var defLoader = DefLoader.Get();
                        if (defLoader == null)
                        {
                            File.AppendAllText(_logoutput, "defLoader is null");
                            File.AppendAllText(_logoutput, Environment.NewLine);
                            return;
                        }
                        var showEnt = (Network.HistShowEntity)history;
                        var entDef = defLoader.GetEntityDef(showEnt.Entity.CardID);

                        File.AppendAllText(_logoutput, $"SHOW_ENTITY - {entDef.GetName()}");
                        File.AppendAllText(_logoutput, Environment.NewLine);
                    }
                    else
                    {
                        File.AppendAllText(_logoutput, history.ToString());
                        File.AppendAllText(_logoutput, Environment.NewLine);
                    }
                });
            }
            catch (Exception e)
            {
                File.AppendAllText(_logoutput, e.Message);
                File.AppendAllText(_logoutput, Environment.NewLine);
                File.AppendAllText(_logoutput, e.StackTrace);
            }
        }

        private void OnDestroy()
        {
            Logger.AppendLine("BrodeStone.OnDestroy()");
        }

        private void OnGUI()
        {
            var goCamera = CameraUtils.FindFirstByLayer(gameObject.layer);

            var pos = CameraUtils.GetPosInFrontOfCamera(goCamera, goCamera.nearClipPlane);
            var positionString = string.Format("{3} - x:{0} y:{1} z:{2}", pos.x, pos.y, pos.z, "goCamera+nearClip");
            GUI.Label(new Rect(10, 60, 100, 100), positionString);

            var currentScene = SceneMgr.Get().GetMode();
            GUI.Label(new Rect(10, 140, 500, 100), $"CurrentScene:{currentScene}");

            var mousePosition = Input.mousePosition;
            mousePosition.z = goCamera.nearClipPlane;
            var mouseWorldPos = goCamera.ScreenToWorldPoint(mousePosition);

            GUI.Label(new Rect(10, 170, 500, 100), $"MouseWorldPos x:{mouseWorldPos.x}, y:{mouseWorldPos.y}, z:{mouseWorldPos.z}");

            GUI.Label(new Rect(10, 200, 500, 100), $"MousePos x:{Input.mousePosition.x}, y:{Input.mousePosition.y}, z:{Input.mousePosition.z}");

        }
    }
}

