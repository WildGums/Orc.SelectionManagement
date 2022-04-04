﻿namespace Orc.SelectionManagement
{
    using System.Linq;
    using Catel;

    public static class ISelectionManagerExtensions
    {
        public static T GetSelectedItem<T>(this ISelectionManager<T> selectionManager, string scope = null)
        {
            Argument.IsNotNull(() => selectionManager);

            return selectionManager.GetSelectedItems(scope).LastOrDefault();
        }

        public static void Add<T>(this ISelectionManager<T> selectionManager, T item, string scope = null)
        {
            Argument.IsNotNull(() => selectionManager);

            if (ReferenceEquals(item, null))
            {
                return;
            }

            selectionManager.Add(new[] { item }, scope);
        }

        public static void Replace<T>(this ISelectionManager<T> selectionManager, T item, string scope = null)
        {
            Argument.IsNotNull(() => selectionManager);

            if (ReferenceEquals(item, null))
            {
                selectionManager.Clear();
                return;
            }

            selectionManager.Replace(new[] { item }, scope);
        }

        public static void Remove<T>(this ISelectionManager<T> selectionManager, T item, string scope = null)
        {
            Argument.IsNotNull(() => selectionManager);

            if (ReferenceEquals(item, null))
            {
                return;
            }

            selectionManager.Remove(new[] { item }, scope);
        }
    }
}
