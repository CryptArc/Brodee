using System;

namespace Brodee
{
    [Flags]
    public enum Scene
    {
        None = 0,
        Hub = 1,
        GamePlay = 2,
        Collection = 4,
        Unknown = 8,
        PackOpening = 16,
        Tournament = 32,
        Friendly = 64,
        Draft = 128,
        Adventure = 256,
        TavernBrawl = 512
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
                    return Scene.Hub;
                case SceneMgr.Mode.GAMEPLAY:
                    return Scene.GamePlay;
                case SceneMgr.Mode.COLLECTIONMANAGER:
                    return Scene.Collection;
                case SceneMgr.Mode.PACKOPENING:
                    return Scene.PackOpening;
                case SceneMgr.Mode.TOURNAMENT:
                    return Scene.Tournament;
                case SceneMgr.Mode.FRIENDLY:
                    return Scene.Friendly;
                case SceneMgr.Mode.FATAL_ERROR:
                    return Scene.Unknown;
                case SceneMgr.Mode.DRAFT:
                    return Scene.Draft;
                case SceneMgr.Mode.CREDITS:
                    return Scene.Unknown;
                case SceneMgr.Mode.RESET:
                    return Scene.Unknown;
                case SceneMgr.Mode.ADVENTURE:
                    return Scene.Adventure;
                case SceneMgr.Mode.TAVERN_BRAWL:
                    return Scene.TavernBrawl;
                default:
                    return Scene.Unknown;
            }
        }
    }
}