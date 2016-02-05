using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Brodee.Controls;
using Brodee.Core;
using Newtonsoft.Json;
using Object = Brodee.Modules.UI.Object;

namespace Brodee.Modules
{
    public class ModuleManager
    {
        const string UiFileLocationFormat = ".brodee\\modules\\{0}\\ui.json";
        const string ConfigFileLocationFormat = ".brodee\\modules\\{0}\\config.json";

        public Dictionary<string, Module> _modules = new Dictionary<string, Module>();

        public void LoadFromConfig(string moduleName)
        {
            var moduleConfigLocation = string.Format(ConfigFileLocationFormat, moduleName);
            var moduleUiLocation = string.Format(UiFileLocationFormat, moduleName);
            if (File.Exists(moduleConfigLocation))
            {

                Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(moduleConfigLocation));
                var newModule = new Module(config.Name);
                if (File.Exists(moduleUiLocation))
                {
                    Object[] uiObjects = JsonConvert.DeserializeObject<Object[]>(File.ReadAllText(moduleConfigLocation));
                    newModule.UiObjects.AddRange(uiObjects);
                }
                _modules.Add(config.Name, newModule);
            }
            else
            {
                Logger.AppendLine($"Unable to load config for module {moduleName}");
            }
        }

        internal void LoadCore(IHandlerHub handlerHub,
            GameMenuControls gameMenuControls,
            OptionMenuControls optionMenuControls,
            GeneralControls generalControls,
            Func<GameState> oldGameStateFunc,
            Func<GameState> newGameStateFunc)
        {
            var coreInstaller = new CoreModuleInstaller(gameMenuControls, optionMenuControls, generalControls, oldGameStateFunc, newGameStateFunc);
            coreInstaller.Install(handlerHub);
        }

        internal void LoadModules(HandlerHub handlerHub)
        {
            foreach (var module in _modules.Select(x => x.Key != "Core"))
            {
                //TODO: Do module loading here
            }
        }
    }
}