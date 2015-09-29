using Brodee.Triggers;
using UnityEngine;

namespace Brodee.Handlers
{
    public class DestroyChildGameObjectsHandler : Handler
    {

        public override Trigger[] SpecificHandle(GameState previous, GameState next)
        {
            var childCount = next.Cubes.Count;
            Logger.AppendLine($"DestroyChildGameObjectsHandler Start childCount:{childCount}");
            if (childCount > 0)
            {
                foreach (GameObject cube in next.Cubes)
                {
                    Object.Destroy(cube);
                }
                next.Cubes.Clear();
            }
            Logger.AppendLine(string.Format("DestroyChildGameObjectsHandler End childCount:{0}", Parent.transform.childCount));
            return EmptyTriggers;
        }
    }
}