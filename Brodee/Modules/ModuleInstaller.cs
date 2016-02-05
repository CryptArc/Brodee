namespace Brodee.Modules
{
    public abstract class ModuleInstaller
    {
        public string Name { get; }
        protected ModuleInstaller(string name)
        {
            Name = name;
        }
    }
}