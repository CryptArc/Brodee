using System.Collections.Generic;
using Brodee.Modules.UI;

namespace Brodee.Modules
{
    public class Module
    {
        public readonly string Name;

        public readonly List<Object> UiObjects = new List<Object>();

        public Module(string name)
        {
            Name = name;
        }

    }
}