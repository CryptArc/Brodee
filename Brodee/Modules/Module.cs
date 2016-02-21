using System.Collections.Generic;
using Brodee.Controls;
using Brodee.Modules.UI;

namespace Brodee.Modules
{
    public class Module
    {
        public readonly string Name;

        public readonly List<UiObject> UiObjects = new List<UiObject>();

        public Module(string name)
        {
            Name = name;
        }

        public void TryLoad(OptionMenuControls optionMenuControls)
        {
            foreach (var uiObject in UiObjects)
            {
                uiObject.TryLoad(optionMenuControls);
            }
        }

        public void Destroy()
        {
            foreach (var uiObject in UiObjects)
            {
                uiObject.Destroy();
            }
        }
    }
}