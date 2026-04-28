namespace Orc.SelectionManagement.Example.Views;

using System;
using System.Linq;
using System.Windows.Controls;
using Catel.Services;
using Catel.Windows;

public partial class MainWindow
{
    private readonly ISelectionManager<int> _intSelectionManager;
    private readonly ISelectionManager<string> _stringSelectionManager;

    public MainWindow(IServiceProvider serviceProvider, IWrapControlService wrapControlService,
        ILanguageService languageService, ISelectionManager<int> intSelectionManager, ISelectionManager<string> stringSelectionManager)
        : base(serviceProvider, wrapControlService, languageService)
    {
        _intSelectionManager = intSelectionManager;
        _stringSelectionManager = stringSelectionManager;

        InitializeComponent();
    }

    private void OnIntsWithoutScopeListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listBox = (ListBox)sender;

        _intSelectionManager.Replace(listBox.SelectedItems.Cast<int>());
    }

    private void OnIntsWithScopeListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listBox = (ListBox)sender;

        _intSelectionManager.Replace(listBox.SelectedItems.Cast<int>(), "scope");
    }

    private void OnStringsWithoutScopeListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listBox = (ListBox)sender;

        _stringSelectionManager.Replace(listBox.SelectedItems.Cast<string>());
    }

    private void OnStringsWithScopeListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listBox = (ListBox)sender;

        _stringSelectionManager.Replace(listBox.SelectedItems.Cast<string>(), "scope");
    }
}
