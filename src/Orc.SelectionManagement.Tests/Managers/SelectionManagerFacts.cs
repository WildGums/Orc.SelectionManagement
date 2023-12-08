namespace Orc.SelectionManagement.Test.Managers;

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
        public void AddMultipleItemsWithMultiSelectEnabledAndEmptyStart(string? scope)
        {
            var addedItems = new List<int>();
            var removedItems = new List<int>();

            var selectionManager = new SelectionManager<int>
            {
                AllowMultiSelect = true
            };

            selectionManager.SelectionChanged += (sender, e) =>
            {
                Assert.That(e.Scope, Is.EqualTo(scope));

                addedItems.AddRange(e.Added);
                removedItems.AddRange(e.Removed);
            };

            selectionManager.Add(new[] { 1, 2, 3 }, scope);

            Assert.That(addedItems.Count, Is.EqualTo(3));
            Assert.That(addedItems[0], Is.EqualTo(1));
            Assert.That(addedItems[1], Is.EqualTo(2));
            Assert.That(addedItems[2], Is.EqualTo(3));

            Assert.That(removedItems.Count, Is.EqualTo(0));

            var selectedItems = selectionManager.GetSelectedItems(scope);

            Assert.That(selectedItems.Length, Is.EqualTo(3));
            Assert.That(selectedItems[0], Is.EqualTo(1));
            Assert.That(selectedItems[1], Is.EqualTo(2));
            Assert.That(selectedItems[2], Is.EqualTo(3));

            var selectedItem = selectionManager.GetSelectedItem(scope);

            Assert.That(selectedItem, Is.EqualTo(3));
        }

        [TestCase(null)]
        [TestCase("A")]
        [TestCase("B")]
        public void AddMultipleItemsWithMultiSelectEnabledAndFilledUpStart(string? scope)
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
                Assert.That(e.Scope, Is.EqualTo(scope));

                addedItems.AddRange(e.Added);
                removedItems.AddRange(e.Removed);
            };

            selectionManager.Add(new[] { 1, 2, 3 }, scope);

            Assert.That(addedItems.Count, Is.EqualTo(3));
            Assert.That(addedItems[0], Is.EqualTo(1));
            Assert.That(addedItems[1], Is.EqualTo(2));
            Assert.That(addedItems[2], Is.EqualTo(3));

            Assert.That(removedItems.Count, Is.EqualTo(0));

            var selectedItems = selectionManager.GetSelectedItems(scope);

            Assert.That(selectedItems.Length, Is.EqualTo(6));
            Assert.That(selectedItems[0], Is.EqualTo(4));
            Assert.That(selectedItems[1], Is.EqualTo(5));
            Assert.That(selectedItems[2], Is.EqualTo(6));
            Assert.That(selectedItems[3], Is.EqualTo(1));
            Assert.That(selectedItems[4], Is.EqualTo(2));
            Assert.That(selectedItems[5], Is.EqualTo(3));

            var selectedItem = selectionManager.GetSelectedItem(scope);

            Assert.That(selectedItem, Is.EqualTo(3));
        }

        [TestCase(null)]
        [TestCase("A")]
        [TestCase("B")]
        public void AddMultipleItemsWithMultiSelectDisabledAndEmptyStart(string? scope)
        {
            var addedItems = new List<int>();
            var removedItems = new List<int>();

            var selectionManager = new SelectionManager<int>
            {
                AllowMultiSelect = false
            };

            selectionManager.SelectionChanged += (sender, e) =>
            {
                Assert.That(e.Scope, Is.EqualTo(scope));

                addedItems.AddRange(e.Added);
                removedItems.AddRange(e.Removed);
            };

            selectionManager.Add(new[] { 1, 2, 3 }, scope);

            Assert.That(addedItems.Count, Is.EqualTo(1));
            Assert.That(addedItems[0], Is.EqualTo(3));

            Assert.That(removedItems.Count, Is.EqualTo(0));

            var selectedItems = selectionManager.GetSelectedItems(scope);

            Assert.That(selectedItems.Length, Is.EqualTo(1));
            Assert.That(selectedItems[0], Is.EqualTo(3));

            var selectedItem = selectionManager.GetSelectedItem(scope);

            Assert.That(selectedItem, Is.EqualTo(3));
        }

        [TestCase(null)]
        [TestCase("A")]
        [TestCase("B")]
        public void AddMultipleItemsWithMultiSelectDisabledAndFilledUpStart(string? scope)
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
                Assert.That(e.Scope, Is.EqualTo(scope));

                addedItems.AddRange(e.Added);
                removedItems.AddRange(e.Removed);
            };

            selectionManager.Add(new[] { 1, 2, 3 }, scope);

            Assert.That(addedItems.Count, Is.EqualTo(1));
            Assert.That(addedItems[0], Is.EqualTo(3));

            Assert.That(removedItems.Count, Is.EqualTo(1));
            Assert.That(removedItems[0], Is.EqualTo(6));

            var selectedItems = selectionManager.GetSelectedItems(scope);

            Assert.That(selectedItems.Length, Is.EqualTo(1));
            Assert.That(selectedItems[0], Is.EqualTo(3));

            var selectedItem = selectionManager.GetSelectedItem(scope);

            Assert.That(selectedItem, Is.EqualTo(3));
        }
    }

    [TestFixture]
    public class TheRemoveMethod
    {
        [TestCase(null)]
        [TestCase("A")]
        [TestCase("B")]
        public void RemoveMultipleItemsWithMultiSelectEnabledAndFilledUpStart_ExistingItems(string? scope)
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
                Assert.That(e.Scope, Is.EqualTo(scope));

                addedItems.AddRange(e.Added);
                removedItems.AddRange(e.Removed);
            };

            selectionManager.Remove(new[] { 4, 5, 6 }, scope);

            Assert.That(addedItems.Count, Is.EqualTo(0));

            Assert.That(removedItems.Count, Is.EqualTo(3));
            Assert.That(removedItems[0], Is.EqualTo(4));
            Assert.That(removedItems[1], Is.EqualTo(5));
            Assert.That(removedItems[2], Is.EqualTo(6));

            var selectedItems = selectionManager.GetSelectedItems(scope);

            Assert.That(selectedItems.Length, Is.EqualTo(0));

            var selectedItem = selectionManager.GetSelectedItem(scope);

            Assert.That(selectedItem, Is.EqualTo(0));
        }

        [TestCase(null)]
        [TestCase("A")]
        [TestCase("B")]
        public void RemoveMultipleItemsWithMultiSelectEnabledAndFilledUpStart_MissingItems(string? scope)
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
                Assert.That(e.Scope, Is.EqualTo(scope));

                addedItems.AddRange(e.Added);
                removedItems.AddRange(e.Removed);
            };

            selectionManager.Remove(new[] { 1, 2, 3 }, scope);

            Assert.That(addedItems.Count, Is.EqualTo(0));
            Assert.That(removedItems.Count, Is.EqualTo(0));

            var selectedItems = selectionManager.GetSelectedItems(scope);

            Assert.That(selectedItems.Length, Is.EqualTo(3));
            Assert.That(selectedItems[0], Is.EqualTo(4));
            Assert.That(selectedItems[1], Is.EqualTo(5));
            Assert.That(selectedItems[2], Is.EqualTo(6));

            var selectedItem = selectionManager.GetSelectedItem(scope);

            Assert.That(selectedItem, Is.EqualTo(6));
        }
    }

    [TestFixture]
    public class TheReplaceMethod
    {
        [TestCase(null)]
        [TestCase("A")]
        [TestCase("B")]
        public void ReplaceMultipleItemsWithMultiSelectEnabledAndFilledUpStart_ExistingItems(string? scope)
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
                Assert.That(e.Scope, Is.EqualTo(scope));

                addedItems.AddRange(e.Added);
                removedItems.AddRange(e.Removed);
            };

            selectionManager.Replace(new[] { 1, 2, 3 }, scope);

            Assert.That(addedItems.Count, Is.EqualTo(3));
            Assert.That(addedItems[0], Is.EqualTo(1));
            Assert.That(addedItems[1], Is.EqualTo(2));
            Assert.That(addedItems[2], Is.EqualTo(3));

            Assert.That(removedItems.Count, Is.EqualTo(3));
            Assert.That(removedItems[0], Is.EqualTo(4));
            Assert.That(removedItems[1], Is.EqualTo(5));
            Assert.That(removedItems[2], Is.EqualTo(6));

            var selectedItems = selectionManager.GetSelectedItems(scope);

            Assert.That(selectedItems.Length, Is.EqualTo(3));
            Assert.That(selectedItems[0], Is.EqualTo(1));
            Assert.That(selectedItems[1], Is.EqualTo(2));
            Assert.That(selectedItems[2], Is.EqualTo(3));

            var selectedItem = selectionManager.GetSelectedItem(scope);

            Assert.That(selectedItem, Is.EqualTo(3));
        }

        [TestCase]
        public void SingleReplaceWithNullShouldClearSelection()
        {
            var addedItems = new List<object>();
            var removedItems = new List<object>();

            var selectionManager = new SelectionManager<object>
            {
                AllowMultiSelect = true
            };

            selectionManager.Add(new[] 
            {
                new object(),
                new object() 
            });

            selectionManager.SelectionChanged += (sender, e) =>
            {
                addedItems.AddRange(e.Added);
                removedItems.AddRange(e.Removed);
            };

            selectionManager.Replace<object>(null);

            Assert.That(selectionManager.GetSelectedItems().Length, Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class TheClearMethod
    {
        [TestCase(null)]
        [TestCase("A")]
        [TestCase("B")]
        public void ClearMultipleItemsWithMultiSelectEnabledAndFilledUpStart_ExistingItems(string? scope)
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
                Assert.That(e.Scope, Is.EqualTo(scope));

                addedItems.AddRange(e.Added);
                removedItems.AddRange(e.Removed);
            };

            selectionManager.Clear(scope);

            Assert.That(removedItems.Count, Is.EqualTo(3));
            Assert.That(removedItems[0], Is.EqualTo(4));
            Assert.That(removedItems[1], Is.EqualTo(5));
            Assert.That(removedItems[2], Is.EqualTo(6));

            var selectedItems = selectionManager.GetSelectedItems(scope);

            Assert.That(selectedItems.Length, Is.EqualTo(0));

            var selectedItem = selectionManager.GetSelectedItem(scope);

            Assert.That(selectedItem, Is.EqualTo(0));
        }
    }
}
