using System;

namespace Brodee.Handlers
{
    [Flags]
    public enum ScenesToProcessOn
    {
        None = 0,
        All = 1 << 0,
        Hub = 1 << 1,
        GamePlay = 1 << 2,
        Collection = 1 << 4
    }
}