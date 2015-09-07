using UnityEngine;

namespace BrodeStone.Handlers
{
    public class DestroyChildGameObjectsHandler : IHandler
    {
        public HandlerType GetHandlerType => HandlerType.DestroyChildGameObjects;

        public void Handle(GameObject component, GameState previous, GameState next)
        {
            var childCount = component.transform.childCount;
            Logger.AppendLine(string.Format("DestroyChildGameObjectsHandler Start childCount:{0}",childCount));
            if (childCount > 0)
            {
                foreach (Transform transform in component.transform)
                {
                    Object.Destroy(transform.gameObject);
                }
                
            }
            Logger.AppendLine(string.Format("DestroyChildGameObjectsHandler End childCount:{0}", component.transform.childCount));
        }
    }
}