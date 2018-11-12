// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectManager.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SelectionManagement
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Catel;
    using Catel.Logging;

    public class SelectionManager<T> : ISelectionManager<T>
    {
        #region Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly object _lockObject = new object();
        private readonly Dictionary<string, SelectionList<T>> _selectionsByScope = new Dictionary<string, SelectionList<T>>();
        private SelectionList<T> _noScopeSelections;

        private bool _allowMultiSelect = true;
        #endregion

        #region Constructors
        public SelectionManager()
        {

        }
        #endregion

        #region Properties
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
        #endregion

        #region Events
        public event EventHandler<SelectionChangedEventArgs<T>> SelectionChanged;
        #endregion

        #region Methods

        public List<T> GetSelectedItems(string scope = null)
        {
            var selectionList = GetSelectionListForScope(scope);
            return selectionList.GetSelectedItems();
        }

        public void Add(IEnumerable<T> items, string scope = null)
        {
            var selectionList = GetSelectionListForScope(scope);
            selectionList.Add(items);
        }

        public void Replace(IEnumerable<T> items, string scope = null)
        {
            var selectionList = GetSelectionListForScope(scope);
            selectionList.Replace(items);
        }

        public void Remove(IEnumerable<T> items, string scope = null)
        {
            var selectionList = GetSelectionListForScope(scope);
            selectionList.Remove(items);
        }

        public void Clear(string scope = null)
        {
            var selectionList = GetSelectionListForScope(scope);
            selectionList.Clear();
        }

        private void EnforceSelectionMode()
        {
            lock (_lockObject)
            {
                var listsToCheck = new List<SelectionList<T>>();

                if (_noScopeSelections != null)
                {
                    listsToCheck.Add(_noScopeSelections);
                }

                listsToCheck.AddRange(_selectionsByScope.Values);

                listsToCheck.ForEach(x => x.AllowMultiSelect = AllowMultiSelect);
            }
        }

        private SelectionList<T> GetSelectionListForScope(string scope)
        {
            lock (_lockObject)
            {
                if (scope is null)
                {
                    if (_noScopeSelections is null)
                    {
                        _noScopeSelections = CreateSelectionList(null);
                    }

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

        private SelectionList<T> CreateSelectionList(string scope)
        {
            var selectionList = new SelectionList<T>(scope)
            {
                AllowMultiSelect = AllowMultiSelect
            };

            selectionList.SelectionChanged += OnSelectionListSelectionChanged;

            return selectionList;
        }

        private void OnSelectionListSelectionChanged(object sender, SelectionChangedEventArgs<T> e)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Selection has changed for scope '{e.Scope}':");

            stringBuilder.AppendLine($"  Added: ({e.Added.Count})");

            foreach (var added in e.Added)
            {
                stringBuilder.AppendLine($"    - ({added})");
            }

            stringBuilder.AppendLine($"  Removed: ({e.Removed.Count})");

            foreach (var removed in e.Removed)
            {
                stringBuilder.AppendLine($"    - ({removed})");
            }

            Log.Debug(stringBuilder.ToString());

            SelectionChanged.SafeInvoke(this, e);
        }
        #endregion
    }
}
