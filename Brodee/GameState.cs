using System.Collections.Generic;
using UnityEngine;

namespace Brodee
{
    public class GameState
    {
        public Handlers.Scene Mode = Handlers.Scene.None;
        public List<KeyCode> KeysPressed = new List<KeyCode>();
        public List<GameObject> Cubes;
    }
}