namespace Orc
{
    using Catel.Services;
    using Catel.ThirdPartyNotices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Orc.SelectionManagement;

    /// <summary>
    /// Core module which allows the registration of default services in the service collection.
    /// </summary>
    public static class OrcSelectionManagementModule
    {
        public static IServiceCollection AddOrcSelectionManagement(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton(typeof(ISelectionManager<>), typeof(SelectionManager<>));

            serviceCollection.AddSingleton<ILanguageSource>(new LanguageResourceSource("Orc.SelectionManagement", "Orc.SelectionManagement.Properties", "Resources"));

            serviceCollection.AddSingleton<IThirdPartyNotice>((x) => new LibraryThirdPartyNotice("Orc.SelectionManagement", "https://github.com/wildgums/orc.selectionmanagement"));

            return serviceCollection;
        }
    }
}
