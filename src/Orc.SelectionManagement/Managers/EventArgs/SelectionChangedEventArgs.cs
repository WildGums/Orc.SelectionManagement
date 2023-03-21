namespace Orc.SelectionManagement;

using System;
using System.Collections.Generic;
using System.Linq;

public class SelectionChangedEventArgs<T> : EventArgs
{
    public SelectionChangedEventArgs(IEnumerable<T>? added, IEnumerable<T>? removed, string? scope)
    {
        Added = added?.ToArray() ?? Array.Empty<T>();
        Removed = removed?.ToArray() ?? Array.Empty<T>();
        Scope = scope;
    }

    public T[] Added { get; private set; }

    public T[] Removed { get; private set; }

    public string? Scope { get; private set; }
}
