namespace Orc.SelectionManagement;

using System;
using System.Collections.Generic;

public interface ISelectionManager<T>
{
    bool AllowMultiSelect { get; set; }

    event EventHandler<SelectionChangedEventArgs<T>>? SelectionChanged;

    T[] GetSelectedItems(string? scope = null);

    void Add(IEnumerable<T> items, string? scope = null);

    void Replace(IEnumerable<T> items, string? scope = null);

    void Remove(IEnumerable<T> items, string? scope = null);

    void Clear(string? scope = null);
}
