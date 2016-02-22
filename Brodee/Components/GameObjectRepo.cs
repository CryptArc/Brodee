using System.Collections.Generic;
using UnityEngine;

namespace Brodee.Components
{
    public class GameObjectRepo
    {
        readonly Dictionary<string, GameObject> _gameObjects = new Dictionary<string, GameObject>();

        public void AddOrUpdate(string name, GameObject gameObject)
        {
            _gameObjects[name] = gameObject;
        }

        public bool TryGet(string name, out GameObject gameObject)
        {
            if (_gameObjects.TryGetValue(name, out gameObject))
            {
                if (gameObject == null)
                {
                    _gameObjects.Remove(name);
                    return false;
                }
            }
            gameObject = null;
            return false;
        }
    }
}