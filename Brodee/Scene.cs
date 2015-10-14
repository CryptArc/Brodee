using System;

namespace Brodee.Handlers
{
    [Flags]
    public enum Scene
    {
        None = 0,
        All = 1, 
        Hub = 2,
        GamePlay = 4,
        Collection = 8,
        Unknown = 16,
        PackOpening = 32,
        Tournament = 64,
        Friendly = 128,
        Draft = 256,
        Adventure = 512,
        TavernBrawl = 1024
    }

    public static class ScenesToProcessOnExtensions
    {
        public static bool IsSet(this Scene scene, Scene flags)
        {
            return (scene & flags) == flags;
        }

        public static Scene MapToScene(this SceneMgr.Mode mode)
        {
            switch (mode)
            {
                case SceneMgr.Mode.INVALID:
                    return Scene.Unknown;
                case SceneMgr.Mode.STARTUP:
                    return Scene.Unknown;
                case SceneMgr.Mode.LOGIN:
                    return Scene.Unknown;
                case SceneMgr.Mode.HUB:
                    return Scene.Hub | Scene.All;
                case SceneMgr.Mode.GAMEPLAY:
                    return Scene.GamePlay | Scene.All;
                case SceneMgr.Mode.COLLECTIONMANAGER:
                    return Scene.Collection | Scene.All;
                case SceneMgr.Mode.PACKOPENING:
                    return Scene.PackOpening | Scene.All;
                case SceneMgr.Mode.TOURNAMENT:
                    return Scene.Tournament | Scene.All;
                case SceneMgr.Mode.FRIENDLY:
                    return Scene.Friendly | Scene.All;
                case SceneMgr.Mode.FATAL_ERROR:
                    return Scene.Unknown;
                case SceneMgr.Mode.DRAFT:
                    return Scene.Draft | Scene.All;
                case SceneMgr.Mode.CREDITS:
                    return Scene.Unknown;
                case SceneMgr.Mode.RESET:
                    return Scene.Unknown;
                case SceneMgr.Mode.ADVENTURE:
                    return Scene.Adventure | Scene.All;
                case SceneMgr.Mode.TAVERN_BRAWL:
                    return Scene.TavernBrawl | Scene.All;
                default:
                    return Scene.Unknown;
            }
        }
    }
}