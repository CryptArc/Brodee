using System.Collections.Generic;
using UnityEngine;

namespace BrodeStone
{
    public class GameState
    {
        public SceneMgr.Mode Mode = SceneMgr.Mode.INVALID;
        public List<KeyCode> KeysPressed = new List<KeyCode>();
        public List<GameObject> Cubes;
    }
}