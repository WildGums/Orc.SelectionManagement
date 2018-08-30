namespace Orc.SelectionManagement.Test.Managers
{
    using System.Collections.Generic;
    using NUnit.Framework;

    public class SelectionManagerFacts
    {
        [TestFixture]
        public class TheAddMethod
        {
            [TestCase(null)]
            [TestCase("A")]
            [TestCase("B")]
            public void AddMultipleItemsWithMultiSelectEnabledAndEmptyStart(string scope)
            {
                var addedItems = new List<int>();
                var removedItems = new List<int>();

                var selectionManager = new SelectionManager<int>
                {
                    AllowMultiSelect = true
                };

                selectionManager.SelectionChanged += (sender, e) =>
                {
                    Assert.AreEqual(scope, e.Scope);

                    addedItems.AddRange(e.Added);
                    removedItems.AddRange(e.Removed);
                };

                selectionManager.Add(new[] { 1, 2, 3 }, scope);

                Assert.AreEqual(3, addedItems.Count);
                Assert.AreEqual(1, addedItems[0]);
                Assert.AreEqual(2, addedItems[1]);
                Assert.AreEqual(3, addedItems[2]);

                Assert.AreEqual(0, removedItems.Count);

                var selectedItems = selectionManager.GetSelectedItems(scope);

                Assert.AreEqual(3, selectedItems.Count);
                Assert.AreEqual(1, selectedItems[0]);
                Assert.AreEqual(2, selectedItems[1]);
                Assert.AreEqual(3, selectedItems[2]);

                var selectedItem = selectionManager.GetSelectedItem(scope);

                Assert.AreEqual(3, selectedItem);
            }

            [TestCase(null)]
            [TestCase("A")]
            [TestCase("B")]
            public void AddMultipleItemsWithMultiSelectEnabledAndFilledUpStart(string scope)
            {
                var addedItems = new List<int>();
                var removedItems = new List<int>();

                var selectionManager = new SelectionManager<int>
                {
                    AllowMultiSelect = true
                };

                selectionManager.Add(new[] { 4, 5, 6 }, scope);

                selectionManager.SelectionChanged += (sender, e) =>
                {
                    Assert.AreEqual(scope, e.Scope);

                    addedItems.AddRange(e.Added);
                    removedItems.AddRange(e.Removed);
                };

                selectionManager.Add(new[] { 1, 2, 3 }, scope);

                Assert.AreEqual(3, addedItems.Count);
                Assert.AreEqual(1, addedItems[0]);
                Assert.AreEqual(2, addedItems[1]);
                Assert.AreEqual(3, addedItems[2]);

                Assert.AreEqual(0, removedItems.Count);

                var selectedItems = selectionManager.GetSelectedItems(scope);

                Assert.AreEqual(6, selectedItems.Count);
                Assert.AreEqual(4, selectedItems[0]);
                Assert.AreEqual(5, selectedItems[1]);
                Assert.AreEqual(6, selectedItems[2]);
                Assert.AreEqual(1, selectedItems[3]);
                Assert.AreEqual(2, selectedItems[4]);
                Assert.AreEqual(3, selectedItems[5]);

                var selectedItem = selectionManager.GetSelectedItem(scope);

                Assert.AreEqual(3, selectedItem);
            }

            [TestCase(null)]
            [TestCase("A")]
            [TestCase("B")]
            public void AddMultipleItemsWithMultiSelectDisabledAndEmptyStart(string scope)
            {
                var addedItems = new List<int>();
                var removedItems = new List<int>();

                var selectionManager = new SelectionManager<int>
                {
                    AllowMultiSelect = false
                };

                selectionManager.SelectionChanged += (sender, e) =>
                {
                    Assert.AreEqual(scope, e.Scope);

                    addedItems.AddRange(e.Added);
                    removedItems.AddRange(e.Removed);
                };

                selectionManager.Add(new[] { 1, 2, 3 }, scope);

                Assert.AreEqual(1, addedItems.Count);
                Assert.AreEqual(3, addedItems[0]);

                Assert.AreEqual(0, removedItems.Count);

                var selectedItems = selectionManager.GetSelectedItems(scope);

                Assert.AreEqual(1, selectedItems.Count);
                Assert.AreEqual(3, selectedItems[0]);

                var selectedItem = selectionManager.GetSelectedItem(scope);

                Assert.AreEqual(3, selectedItem);
            }

            [TestCase(null)]
            [TestCase("A")]
            [TestCase("B")]
            public void AddMultipleItemsWithMultiSelectDisabledAndFilledUpStart(string scope)
            {
                var addedItems = new List<int>();
                var removedItems = new List<int>();

                var selectionManager = new SelectionManager<int>
                {
                    AllowMultiSelect = false
                };

                selectionManager.Add(new[] { 4, 5, 6 }, scope);

                selectionManager.SelectionChanged += (sender, e) =>
                {
                    Assert.AreEqual(scope, e.Scope);

                    addedItems.AddRange(e.Added);
                    removedItems.AddRange(e.Removed);
                };

                selectionManager.Add(new[] { 1, 2, 3 }, scope);

                Assert.AreEqual(1, addedItems.Count);
                Assert.AreEqual(3, addedItems[0]);

                Assert.AreEqual(1, removedItems.Count);
                Assert.AreEqual(6, removedItems[0]);

                var selectedItems = selectionManager.GetSelectedItems(scope);

                Assert.AreEqual(1, selectedItems.Count);
                Assert.AreEqual(3, selectedItems[0]);

                var selectedItem = selectionManager.GetSelectedItem(scope);

                Assert.AreEqual(3, selectedItem);
            }
        }

        [TestFixture]
        public class TheRemoveMethod
        {
            [TestCase(null)]
            [TestCase("A")]
            [TestCase("B")]
            public void RemoveMultipleItemsWithMultiSelectEnabledAndFilledUpStart_ExistingItems(string scope)
            {
                var addedItems = new List<int>();
                var removedItems = new List<int>();

                var selectionManager = new SelectionManager<int>
                {
                    AllowMultiSelect = true
                };

                selectionManager.Add(new[] { 4, 5, 6 }, scope);

                selectionManager.SelectionChanged += (sender, e) =>
                {
                    Assert.AreEqual(scope, e.Scope);

                    addedItems.AddRange(e.Added);
                    removedItems.AddRange(e.Removed);
                };

                selectionManager.Remove(new[] { 4, 5, 6 }, scope);

                Assert.AreEqual(0, addedItems.Count);

                Assert.AreEqual(3, removedItems.Count);
                Assert.AreEqual(4, removedItems[0]);
                Assert.AreEqual(5, removedItems[1]);
                Assert.AreEqual(6, removedItems[2]);

                var selectedItems = selectionManager.GetSelectedItems(scope);

                Assert.AreEqual(0, selectedItems.Count);

                var selectedItem = selectionManager.GetSelectedItem(scope);

                Assert.AreEqual(0, selectedItem);
            }

            [TestCase(null)]
            [TestCase("A")]
            [TestCase("B")]
            public void RemoveMultipleItemsWithMultiSelectEnabledAndFilledUpStart_MissingItems(string scope)
            {
                var addedItems = new List<int>();
                var removedItems = new List<int>();

                var selectionManager = new SelectionManager<int>
                {
                    AllowMultiSelect = true
                };

                selectionManager.Add(new[] { 4, 5, 6 }, scope);

                selectionManager.SelectionChanged += (sender, e) =>
                {
                    Assert.AreEqual(scope, e.Scope);

                    addedItems.AddRange(e.Added);
                    removedItems.AddRange(e.Removed);
                };

                selectionManager.Remove(new[] { 1, 2, 3 }, scope);

                Assert.AreEqual(0, addedItems.Count);
                Assert.AreEqual(0, removedItems.Count);

                var selectedItems = selectionManager.GetSelectedItems(scope);

                Assert.AreEqual(3, selectedItems.Count);
                Assert.AreEqual(4, selectedItems[0]);
                Assert.AreEqual(5, selectedItems[1]);
                Assert.AreEqual(6, selectedItems[2]);

                var selectedItem = selectionManager.GetSelectedItem(scope);

                Assert.AreEqual(6, selectedItem);
            }
        }

        [TestFixture]
        public class TheReplaceMethod
        {
            [TestCase(null)]
            [TestCase("A")]
            [TestCase("B")]
            public void ReplaceMultipleItemsWithMultiSelectEnabledAndFilledUpStart_ExistingItems(string scope)
            {
                var addedItems = new List<int>();
                var removedItems = new List<int>();

                var selectionManager = new SelectionManager<int>
                {
                    AllowMultiSelect = true
                };

                selectionManager.Add(new[] { 4, 5, 6 }, scope);

                selectionManager.SelectionChanged += (sender, e) =>
                {
                    Assert.AreEqual(scope, e.Scope);

                    addedItems.AddRange(e.Added);
                    removedItems.AddRange(e.Removed);
                };

                selectionManager.Replace(new[] { 1, 2, 3 }, scope);

                Assert.AreEqual(3, addedItems.Count);
                Assert.AreEqual(1, addedItems[0]);
                Assert.AreEqual(2, addedItems[1]);
                Assert.AreEqual(3, addedItems[2]);

                Assert.AreEqual(3, removedItems.Count);
                Assert.AreEqual(4, removedItems[0]);
                Assert.AreEqual(5, removedItems[1]);
                Assert.AreEqual(6, removedItems[2]);

                var selectedItems = selectionManager.GetSelectedItems(scope);

                Assert.AreEqual(3, selectedItems.Count);
                Assert.AreEqual(1, selectedItems[0]);
                Assert.AreEqual(2, selectedItems[1]);
                Assert.AreEqual(3, selectedItems[2]);

                var selectedItem = selectionManager.GetSelectedItem(scope);

                Assert.AreEqual(3, selectedItem);
            }
        }

        [TestFixture]
        public class TheClearMethod
        {
            [TestCase(null)]
            [TestCase("A")]
            [TestCase("B")]
            public void ClearMultipleItemsWithMultiSelectEnabledAndFilledUpStart_ExistingItems(string scope)
            {
                var addedItems = new List<int>();
                var removedItems = new List<int>();

                var selectionManager = new SelectionManager<int>
                {
                    AllowMultiSelect = true
                };

                selectionManager.Add(new[] { 4, 5, 6 }, scope);

                selectionManager.SelectionChanged += (sender, e) =>
                {
                    Assert.AreEqual(scope, e.Scope);

                    addedItems.AddRange(e.Added);
                    removedItems.AddRange(e.Removed);
                };

                selectionManager.Clear(scope);

                Assert.AreEqual(3, removedItems.Count);
                Assert.AreEqual(4, removedItems[0]);
                Assert.AreEqual(5, removedItems[1]);
                Assert.AreEqual(6, removedItems[2]);

                var selectedItems = selectionManager.GetSelectedItems(scope);

                Assert.AreEqual(0, selectedItems.Count);
                
                var selectedItem = selectionManager.GetSelectedItem(scope);

                Assert.AreEqual(0, selectedItem);
            }
        }
    }
}
