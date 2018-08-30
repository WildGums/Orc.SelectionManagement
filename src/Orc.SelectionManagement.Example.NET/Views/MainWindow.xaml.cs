// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.SelectionManagement.Example.Views
{
    using System.Linq;
    using System.Windows.Controls;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.Windows;

    public partial class MainWindow : DataWindow
    {
        private readonly ISelectionManager<int> _intSelectionManager;
        private readonly ISelectionManager<string> _stringSelectionManager;

        #region Constructors
        public MainWindow()
            : base(DataWindowMode.Custom)
        {
            InitializeComponent();

            logViewer.Level = LogEvent.Debug | LogEvent.Info | LogEvent.Warning | LogEvent.Error;

            var serviceLocator = this.GetServiceLocator();

            _intSelectionManager = serviceLocator.ResolveType<ISelectionManager<int>>();
            _stringSelectionManager = serviceLocator.ResolveType<ISelectionManager<string>>();
        }
        #endregion

        private void OnIntsWithoutScopeListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;

            _intSelectionManager.Replace(listBox.SelectedItems.Cast<int>());
        }

        private void OnIntsWithScopeListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;

            _intSelectionManager.Replace(listBox.SelectedItems.Cast<int>(), "scope");
        }

        private void OnStringsWithoutScopeListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;

            _stringSelectionManager.Replace(listBox.SelectedItems.Cast<string>());
        }

        private void OnStringsWithScopeListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;

            _stringSelectionManager.Replace(listBox.SelectedItems.Cast<string>(), "scope");
        }
    }
}
