using SimpleInjector;

namespace SimpleMicroserviceRunner.Plugin.DependencyInjection
{
    public interface IContainerRegistry
    {
        void Register(Container simpleInjectorContainer);
    }
}
