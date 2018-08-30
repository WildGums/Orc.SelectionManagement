[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.6", FrameworkDisplayName=".NET Framework 4.6")]
public class static ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.SelectionManagement
{
    public interface ISelectionManager<T>
    {
        public event System.EventHandler<Orc.SelectionManagement.SelectionChangedEventArgs<T>> SelectionChanged;
        void Add(System.Collections.Generic.IEnumerable<T> items, string scope = null);
        void Clear(string scope = null);
        System.Collections.Generic.List<T> GetSelectedItems(string scope = null);
        void Remove(System.Collections.Generic.IEnumerable<T> items, string scope = null);
        void Replace(System.Collections.Generic.IEnumerable<T> items, string scope = null);
    }
    public class static ISelectionManagerExtensions
    {
        public static void Add<T>(this Orc.SelectionManagement.ISelectionManager<T> selectionManager, T item, string scope = null) { }
        public static T GetSelectedItem<T>(this Orc.SelectionManagement.ISelectionManager<T> selectionManager, string scope = null) { }
        public static void Remove<T>(this Orc.SelectionManagement.ISelectionManager<T> selectionManager, T item, string scope = null) { }
    }
    public class SelectionChangedEventArgs<T> : System.EventArgs
    {
        public SelectionChangedEventArgs(System.Collections.Generic.IEnumerable<T> added, System.Collections.Generic.IEnumerable<T> removed, string scope) { }
        public System.Collections.Generic.List<T> Added { get; }
        public System.Collections.Generic.List<T> Removed { get; }
        public string Scope { get; }
    }
    public class SelectionManager<T> : Orc.SelectionManagement.ISelectionManager<T>
    {
        public SelectionManager() { }
        public bool AllowMultiSelect { get; set; }
        public event System.EventHandler<Orc.SelectionManagement.SelectionChangedEventArgs<T>> SelectionChanged;
        public void Add(System.Collections.Generic.IEnumerable<T> items, string scope = null) { }
        public void Clear(string scope = null) { }
        public System.Collections.Generic.List<T> GetSelectedItems(string scope = null) { }
        public void Remove(System.Collections.Generic.IEnumerable<T> items, string scope = null) { }
        public void Replace(System.Collections.Generic.IEnumerable<T> items, string scope = null) { }
    }
}