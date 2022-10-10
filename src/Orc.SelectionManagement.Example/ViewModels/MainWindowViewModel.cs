namespace Orc.SelectionManagement.Example.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Collections;
    using Catel.Logging;
    using Catel.MVVM;

    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ISelectionManager<int> _intSelectionManager;
        private readonly ISelectionManager<string> _stringSelectionManager;
        #endregion

        #region Constructors
        public MainWindowViewModel(ISelectionManager<int> intSelectionManager, ISelectionManager<string> stringSelectionManager)
        {
            ArgumentNullException.ThrowIfNull(intSelectionManager);
            ArgumentNullException.ThrowIfNull(stringSelectionManager);

            _intSelectionManager = intSelectionManager;
            _stringSelectionManager = stringSelectionManager;

            Title = "Orc.SelectionManagement example";

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

            SelectedStringsWithoutScope = new ObservableCollection<string>();
            SelectedStringsWithScope = new ObservableCollection<string>();
            SelectedIntsWithoutScope = new ObservableCollection<int>();
            SelectedIntsWithScope = new ObservableCollection<int>();
        }
        #endregion

        #region Properties
        public bool AllowMultiSelect { get; set; }

        public List<string> Strings { get; private set; }

        public ObservableCollection<string> SelectedStringsWithoutScope { get; private set; }

        public ObservableCollection<string> SelectedStringsWithScope { get; private set; }

        public List<int> Ints { get; private set; }

        public ObservableCollection<int> SelectedIntsWithoutScope { get; private set; }

        public ObservableCollection<int> SelectedIntsWithScope { get; private set; }
        #endregion

        #region Methods
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
        #endregion
    }
}
