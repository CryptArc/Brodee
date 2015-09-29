namespace Brodee
{
    public class Loader
    {
        public static void Load()
        {
            var utils = SceneMgr.Get().gameObject.GetComponent("Brodee");
            if (utils == null)
                SceneMgr.Get().gameObject.AddComponent<Brodee>();
        }

        public static void Unload()
        {
            var utils = SceneMgr.Get().gameObject.GetComponent("Brodee");
            if (utils != null)
            {
                UnityEngine.Object.DestroyImmediate(utils);
            }
        }
    }
}
