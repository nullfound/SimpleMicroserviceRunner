using SimpleInjector;
using SimpleMicroserviceRunner.Plugin.DependencyInjection;

namespace SimpleMicroserviceRunner.Sample.BasicWithDI
{
    public class ContainerRegistry : IContainerRegistry
    {
        public void Register(Container simpleInjectorContainer)
        {
            simpleInjectorContainer.Register<IRandomNumberCreator, RandomNumberCreator>(Lifestyle.Singleton);
            simpleInjectorContainer.Register<IRandomNumberRangeProvider, RandomNumberRangeProvider>(Lifestyle.Singleton);
        }
    }
}
