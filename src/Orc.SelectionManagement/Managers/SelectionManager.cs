namespace Orc.SelectionManagement;

using System;
using System.Collections.Generic;
using System.Text;
using Catel.Logging;

public class SelectionManager<T> : ISelectionManager<T>
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    private readonly object _lockObject = new();
    private readonly Dictionary<string, SelectionList<T>> _selectionsByScope = new();
    private readonly SelectionList<T> _noScopeSelections;

    private bool _allowMultiSelect = true;

    public SelectionManager()
    {
        _noScopeSelections = CreateSelectionList(null);
    }

    public bool AllowMultiSelect
    {
        get { return _allowMultiSelect; }
        set
        {
            if (_allowMultiSelect != value)
            {
                Log.Debug($"Updating 'AllowMultiSelect' to '{value}'");

                _allowMultiSelect = value;

                EnforceSelectionMode();
            }
        }
    }

    public event EventHandler<SelectionChangedEventArgs<T>>? SelectionChanged;

    public T[] GetSelectedItems(string? scope = null)
    {
        var selectionList = GetSelectionListForScope(scope);
        return selectionList.GetSelectedItems();
    }

    public void Add(IEnumerable<T> items, string? scope = null)
    {
        var selectionList = GetSelectionListForScope(scope);
        selectionList.Add(items);
    }

    public void Replace(IEnumerable<T> items, string? scope = null)
    {
        var selectionList = GetSelectionListForScope(scope);
        selectionList.Replace(items);
    }

    public void Remove(IEnumerable<T> items, string? scope = null)
    {
        var selectionList = GetSelectionListForScope(scope);
        selectionList.Remove(items);
    }

    public void Clear(string? scope = null)
    {
        var selectionList = GetSelectionListForScope(scope);
        selectionList.Clear();
    }

    private void EnforceSelectionMode()
    {
        lock (_lockObject)
        {
            var listsToCheck = new List<SelectionList<T>>
            {
                _noScopeSelections
            };

            listsToCheck.AddRange(_selectionsByScope.Values);

            listsToCheck.ForEach(x => x.AllowMultiSelect = AllowMultiSelect);
        }
    }

    private SelectionList<T> GetSelectionListForScope(string? scope)
    {
        lock (_lockObject)
        {
            if (scope is null)
            {
                return _noScopeSelections;
            }
            else
            {
                if (!_selectionsByScope.TryGetValue(scope, out var selections))
                {
                    selections = CreateSelectionList(scope);

                    _selectionsByScope[scope] = selections;
                }

                return selections;
            }
        }
    }

    private SelectionList<T> CreateSelectionList(string? scope)
    {
        var selectionList = new SelectionList<T>(scope)
        {
            AllowMultiSelect = AllowMultiSelect
        };

        selectionList.SelectionChanged += OnSelectionListSelectionChanged;

        return selectionList;
    }

    private void OnSelectionListSelectionChanged(object? sender, SelectionChangedEventArgs<T> e)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"Selection has changed for scope '{e.Scope}':");

        stringBuilder.AppendLine($"  Added: ({e.Added.Length})");

        foreach (var added in e.Added)
        {
            stringBuilder.AppendLine($"    - ({added})");
        }

        stringBuilder.AppendLine($"  Removed: ({e.Removed.Length})");

        foreach (var removed in e.Removed)
        {
            stringBuilder.AppendLine($"    - ({removed})");
        }

        Log.Debug(stringBuilder.ToString());

        SelectionChanged?.Invoke(this, e);
    }
}
