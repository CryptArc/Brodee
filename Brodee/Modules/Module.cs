namespace Brodee.Modules
{
    public class Module
    {
        public readonly string Name;
        public readonly Config Config;

        public Module(string name, Config config)
        {
            Name = name;
            Config = config;
        }
    }
}