using Catel.IoC;
using Catel.Services;
using Orc.SelectionManagement;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        // Since Catel doesn't support open generics (yet?), add some defaults
        serviceLocator.RegisterType(typeof(ISelectionManager<>), typeof(SelectionManager<>));
        //serviceLocator.RegisterType(typeof(ISelectionManager<object>), typeof(SelectionManager<object>));
        //serviceLocator.RegisterType(typeof(ISelectionManager<int>), typeof(SelectionManager<int>));

        var languageService = serviceLocator.ResolveType<ILanguageService>();
        languageService.RegisterLanguageSource(new LanguageResourceSource("Orc.SelectionManagement", "Orc.SelectionManagement.Properties", "Resources"));
    }
}
