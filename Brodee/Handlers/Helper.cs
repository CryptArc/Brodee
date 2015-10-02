using UnityEngine;

namespace Brodee.Handlers
{
    public static class Helper
    {
        public static void LogGameObjectComponents(GameObject gameObject)
        {
            Logger.AppendLine($"LogGameObjectComponents Name:{gameObject.name}");
            var comps = gameObject.GetComponents<Component>();
            foreach (var component in comps)
            {
                Logger.AppendLine($"component type:{component.GetType()}");
            }

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Logger.AppendLine($"child go name:{gameObject.transform.GetChild(i).name}");
            }
        }
    }
}