using UnityEngine;

namespace BrodeStone
{
    public static class KeyPressedHelper
    {
        public static void PopulateGameState(GameState gameState)
        {
            if (Input.GetKeyDown(KeyCode.F3))
                gameState.KeysPressed.Add(KeyCode.F3);
            if (Input.GetKeyDown(KeyCode.F4))
                gameState.KeysPressed.Add(KeyCode.F4);
            if (Input.GetKeyDown(KeyCode.F5))
                gameState.KeysPressed.Add(KeyCode.F5);
            if (Input.GetKeyDown(KeyCode.F6))
                gameState.KeysPressed.Add(KeyCode.F6);
            if (Input.GetKeyDown(KeyCode.F7))
                gameState.KeysPressed.Add(KeyCode.F7);
            if (Input.GetKeyDown(KeyCode.F8))
                gameState.KeysPressed.Add(KeyCode.F8);
            if (Input.GetKeyDown(KeyCode.F9))
                gameState.KeysPressed.Add(KeyCode.F9);
            if (Input.GetKeyDown(KeyCode.F10))
                gameState.KeysPressed.Add(KeyCode.F10);
            if (Input.GetKeyDown(KeyCode.F11))
                gameState.KeysPressed.Add(KeyCode.F11);
            if (Input.GetKeyDown(KeyCode.F12))
                gameState.KeysPressed.Add(KeyCode.F12);
        }
    }
}