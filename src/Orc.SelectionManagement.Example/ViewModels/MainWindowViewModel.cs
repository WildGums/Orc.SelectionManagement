namespace Orc.SelectionManagement.Example.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Catel.Collections;
using Catel.MVVM;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ISelectionManager<int> _intSelectionManager;
    private readonly ISelectionManager<string> _stringSelectionManager;

    public MainWindowViewModel(IServiceProvider serviceProvider, 
        ISelectionManager<int> intSelectionManager, ISelectionManager<string> stringSelectionManager)
        : base(serviceProvider)
    {
        _intSelectionManager = intSelectionManager;
        _stringSelectionManager = stringSelectionManager;

        var strings = new List<string>();
        var ints = new List<int>();

        for (var i = 1; i <= 10; i++)
        {
            strings.Add($"{i}");
            ints.Add(i);
        }

        AllowMultiSelect = true;
        Strings = strings;
        Ints = ints;

        SelectedStringsWithoutScope = new System.Collections.ObjectModel.ObservableCollection<string>();
        SelectedStringsWithScope = new System.Collections.ObjectModel.ObservableCollection<string>();
        SelectedIntsWithoutScope = new System.Collections.ObjectModel.ObservableCollection<int>();
        SelectedIntsWithScope = new System.Collections.ObjectModel.ObservableCollection<int>();
    }

    public override string Title => "Orc.SelectionManagement example";

    public bool AllowMultiSelect { get; set; }

    public List<string> Strings { get; }

    public System.Collections.ObjectModel.ObservableCollection<string> SelectedStringsWithoutScope { get; }

    public System.Collections.ObjectModel.ObservableCollection<string> SelectedStringsWithScope { get; }

    public List<int> Ints { get; }

    public System.Collections.ObjectModel.ObservableCollection<int> SelectedIntsWithoutScope { get; }

    public System.Collections.ObjectModel.ObservableCollection<int> SelectedIntsWithScope { get; }

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _intSelectionManager.SelectionChanged += OnIntSelectionManagerSelectionChanged;
        _stringSelectionManager.SelectionChanged += OnStringSelectionManagerSelectionChanged;
    }

    protected override Task CloseAsync()
    {
        _intSelectionManager.SelectionChanged -= OnIntSelectionManagerSelectionChanged;
        _stringSelectionManager.SelectionChanged -= OnStringSelectionManagerSelectionChanged;

        return base.CloseAsync();
    }

    private void OnAllowMultiSelectChanged()
    {
        _intSelectionManager.AllowMultiSelect = AllowMultiSelect;
        _stringSelectionManager.AllowMultiSelect = AllowMultiSelect;
    }

    private void OnStringSelectionManagerSelectionChanged(object sender, SelectionChangedEventArgs<string> e)
    {
        var selectedItems = _stringSelectionManager.GetSelectedItems(e.Scope);

        if (string.IsNullOrWhiteSpace(e.Scope))
        {
            SelectedStringsWithoutScope.ReplaceRange(selectedItems);
        }
        else
        {
            SelectedStringsWithScope.ReplaceRange(selectedItems);
        }
    }

    private void OnIntSelectionManagerSelectionChanged(object sender, SelectionChangedEventArgs<int> e)
    {
        var selectedItems = _intSelectionManager.GetSelectedItems(e.Scope);

        if (string.IsNullOrWhiteSpace(e.Scope))
        {
            SelectedIntsWithoutScope.ReplaceRange(selectedItems);
        }
        else
        {
            SelectedIntsWithScope.ReplaceRange(selectedItems);
        }
    }
}
