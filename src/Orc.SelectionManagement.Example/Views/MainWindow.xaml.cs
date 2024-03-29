﻿namespace Orc.SelectionManagement.Example.Views;

using System.Linq;
using System.Windows.Controls;
using Catel.IoC;
using Catel.Logging;
using Catel.Windows;

public partial class MainWindow : DataWindow
{
    private readonly ISelectionManager<int> _intSelectionManager;
    private readonly ISelectionManager<string> _stringSelectionManager;

    public MainWindow()
        : base(DataWindowMode.Custom)
    {
        InitializeComponent();

        logViewer.Level = LogEvent.Debug | LogEvent.Info | LogEvent.Warning | LogEvent.Error;

#pragma warning disable IDISP001 // Dispose created
        var serviceLocator = this.GetServiceLocator();
#pragma warning restore IDISP001 // Dispose created

        _intSelectionManager = serviceLocator.ResolveType<ISelectionManager<int>>();
        _stringSelectionManager = serviceLocator.ResolveType<ISelectionManager<string>>();
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
