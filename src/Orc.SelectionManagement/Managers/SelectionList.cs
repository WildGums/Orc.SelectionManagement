namespace Orc.SelectionManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class SelectionList<T>
    {
        private readonly List<T> _selectionsList = new List<T>();

        private bool _allowMultiSelect;

        public SelectionList(string? scope)
        {
            Scope = scope;
        }

        public string? Scope { get; private set; }

        public bool AllowMultiSelect
        {
            get { return _allowMultiSelect; }
            set
            {
                if (_allowMultiSelect != value)
                {
                    _allowMultiSelect = value;

                    EnforceSelectionMode();
                }
            }
        }

        public event EventHandler<SelectionChangedEventArgs<T>>? SelectionChanged;

        public T[] GetSelectedItems()
        {
            lock (_selectionsList)
            {
                return _selectionsList.ToArray();
            }
        }
        
        public void Add(IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(items);

            lock (_selectionsList)
            {
                var itemsToAdd = items.Where(x => !ReferenceEquals(x, null)).ToList();
                if (itemsToAdd.Count() == 0)
                {
                    return;
                }

                var addedItems = new List<T>();
                var removedItems = new List<T>();

                if (!AllowMultiSelect)
                {
                    var last = itemsToAdd.Last();
                    
                    // TODO: We could check for existence in the collection as well

                    removedItems.AddRange(_selectionsList);
                    addedItems.Add(last);

                    _selectionsList.Clear();
                    _selectionsList.Add(last);
                }
                else
                {
                    foreach (var item in itemsToAdd)
                    {
                        if (!_selectionsList.Contains(item))
                        {
                            _selectionsList.Add(item);
                            addedItems.Add(item);
                        }
                    }
                }

                if (addedItems.Count == 0)
                {
                    return;
                }

                RaiseSelectionChangedEvent(addedItems, removedItems);
            }
        }

        public void Replace(IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(items);

            lock (_selectionsList)
            {
                var addedItems = new List<T>();
                var removedItems = new List<T>();

                removedItems.AddRange(_selectionsList);
                _selectionsList.Clear();

                var itemsToAdd = items.Where(x => !ReferenceEquals(x, null)).ToList();
                if (itemsToAdd.Count > 0)
                {
                    if (!AllowMultiSelect)
                    {
                        var last = itemsToAdd.Last();
                        if (ReferenceEquals(last, null))
                        {
                            return;
                        }

                        _selectionsList.Add(last);
                        addedItems.Add(last);
                    }
                    else
                    {
                        _selectionsList.AddRange(itemsToAdd);
                        addedItems.AddRange(itemsToAdd);
                    }
                }

                RaiseSelectionChangedEvent(addedItems, removedItems);
            }
        }

        public void Remove(IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(items);

            lock (_selectionsList)
            {
                if (items.Count() == 0)
                {
                    return;
                }

                var removedItems = new List<T>();

                foreach (var item in items)
                {
                    if (!ReferenceEquals(item, null) && _selectionsList.Remove(item))
                    {
                        removedItems.Add(item);
                    }
                }

                if (removedItems.Count == 0)
                {
                    return;
                }

                RaiseSelectionChangedEvent(null, removedItems);
            }
        }

        public void Clear()
        {
            lock (_selectionsList)
            {
                if (_selectionsList.Count == 0)
                {
                    return;
                }

                var selectedItems = _selectionsList.ToList();
                _selectionsList.Clear();

                RaiseSelectionChangedEvent(null, selectedItems);
            }
        }

        private void RaiseSelectionChangedEvent(IEnumerable<T>? added, IEnumerable<T>? removed)
        {
            SelectionChanged?.Invoke(this, new SelectionChangedEventArgs<T>(added, removed, Scope));
        }

        private void EnforceSelectionMode()
        {
            lock (_selectionsList)
            {
                if (!AllowMultiSelect)
                {
                    if (_selectionsList.Count > 1)
                    {
                        var removedItems = _selectionsList.ToList();
                        var lastSelectedItem = removedItems.Last();
                        removedItems.RemoveAt(removedItems.Count - 1);

                        _selectionsList.Clear();
                        _selectionsList.Add(lastSelectedItem);

                        RaiseSelectionChangedEvent(null, removedItems);
                    }
                }
            }
        }
    }
}
