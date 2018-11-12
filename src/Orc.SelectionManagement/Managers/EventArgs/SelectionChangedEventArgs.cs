namespace Orc.SelectionManagement
{
    using System;
    using System.Collections.Generic;

    public class SelectionChangedEventArgs<T> : EventArgs
    {
        public SelectionChangedEventArgs(IEnumerable<T> added, IEnumerable<T> removed, string scope)
        {
            Added = new List<T>();
            Removed = new List<T>();
            Scope = scope;

            if (added != null)
            {
                Added.AddRange(added);
            }

            if (removed != null)
            {
                Removed.AddRange(removed);
            }
        }

        public List<T> Added { get; private set; }

        public List<T> Removed { get; private set; }

        public string Scope { get; private set; }
    }
}
