using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Brodee.Components;
using Brodee.Controls;
using Brodee.Core;
using Newtonsoft.Json;

namespace Brodee.Modules
{
    public class ModuleManager
    {
        const string ConfigFileLocationFormat = ".brodee\\modules\\{0}\\config.json";

        public Dictionary<string, Module> _modules = new Dictionary<string, Module>();

        public void LoadFromConfig(string moduleName)
        {
            var moduleConfigLocation = string.Format(ConfigFileLocationFormat, moduleName);
            if (File.Exists(moduleConfigLocation))
            {
                Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(moduleConfigLocation));
                var newModule = new Module(config.Name, config);
                _modules.Add(config.Name, newModule);
            }
            else
            {
                Logger.AppendLine($"Unable to load config for module {moduleName}");
            }
        }

        internal void LoadCore(GameObjectRepo gameObjectRepo,
            HandlerHub handlerHub,
            GameMenuControls gameMenuControls,
            OptionMenuControls optionMenuControls,
            GeneralControls generalControls,
            Func<GameState> oldGameStateFunc,
            Func<GameState> newGameStateFunc)
        {
            var coreInstaller = new CoreModuleInstaller(gameObjectRepo, gameMenuControls, optionMenuControls, generalControls, oldGameStateFunc, newGameStateFunc, handlerHub);
            coreInstaller.Install(handlerHub);
        }

        internal void LoadModules(HandlerHub handlerHub)
        {
            var workingDirectory = Environment.CurrentDirectory;
            var modulesDir = Path.Combine(workingDirectory, "/Modules/");
            foreach (var module in _modules.Where(x => x.Key != "Core"))
            {
                var assembly = Assembly.LoadFile(module.Value.Config.PathToAssembly);
                var moduleInstallers = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(ModuleInstaller))).ToArray();
                if (moduleInstallers.Length > 1)
                {
                    Logger.AppendLine($"You can only have 1 ModuleInstaller per Assembly. Name:{module.Key}");
                    continue;
                }
                else if (moduleInstallers.Length == 0)
                {
                    Logger.AppendLine($"You have no ModuleInstallers in the {module.Key} Assembly");
                }
            }
        }
    }
}