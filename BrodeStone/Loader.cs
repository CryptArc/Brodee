using UnityEngine;

namespace BrodeStone
{
    public class Loader
    {
        public static void Load()
        {
            var utils = SceneMgr.Get().gameObject.GetComponent("BrodeStone");
            if (utils == null)
                SceneMgr.Get().gameObject.AddComponent<BrodeStone>();
        }

        public static void Unload()
        {
            var utils = SceneMgr.Get().gameObject.GetComponent("BrodeStone");
            if (utils != null)
            {
                UnityEngine.Object.DestroyImmediate(utils);
            }
        }
    }
}
