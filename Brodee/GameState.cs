using System.Collections.Generic;
using Brodee.Handlers;
using UnityEngine;

namespace Brodee
{
    public class GameState
    {
        public Handlers.Scene Mode = Handlers.Scene.None;
        public List<KeyCode> KeysPressed = new List<KeyCode>();
        public List<GameObject> Cubes;
        public GameMenuControls GameMenuControls = new GameMenuControls();
        public OptionMenuControls OptionMenuControls = new OptionMenuControls();
        public GeneralControls GeneralControls = new GeneralControls();
    }
}