namespace Orc.SelectionManagement.Tests.Managers
{
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;

    public partial class ISelectionManagerExtensionsFacts
    {
        [TestFixture]
        public class The_Replace_Method
        {
            [Test]
            public async Task Replaces_Selection_With_Value_Async()
            {
                var selectionManager = new SelectionManager<object>();

                selectionManager.Add(new object());

                Assert.That(selectionManager.GetSelectedItems().Length, Is.EqualTo(1));

                var newObject = new object();

                ISelectionManagerExtensions.Replace(selectionManager, newObject);

                var selectedItems = selectionManager.GetSelectedItems();
                Assert.That(selectedItems.Length, Is.EqualTo(1));
                Assert.That(ReferenceEquals(selectedItems[0], newObject), Is.True);
            }

            [Test]
            public async Task Clears_Selection_With_Null_Value_Async()
            {
                var selectionManager = new SelectionManager<object>();

                selectionManager.Add(new object());

                Assert.That(selectionManager.GetSelectedItems().Length, Is.EqualTo(1));

                ISelectionManagerExtensions.Replace(selectionManager, null);

                Assert.That(selectionManager.GetSelectedItems().Length, Is.EqualTo(0));
            }
        }
    }
}
