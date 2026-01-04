namespace Orc.SelectionManagement.Tests
{
    using Catel;
    using Microsoft.Extensions.DependencyInjection;
    using Orc.SelectionManagement;

    internal static class ServiceCollectionHelper
    {
        public static IServiceCollection CreateServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging();
            serviceCollection.AddCatelCore();
            serviceCollection.AddOrcSelectionManagement();

            return serviceCollection;
        }
    }
}
