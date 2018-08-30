namespace Orc.SelectionManagement.Tests
{
    using System.Runtime.CompilerServices;
    using ApiApprover;
    using NUnit.Framework;

    [TestFixture]
    public class PublicApiFacts
    {
        [Test, MethodImpl(MethodImplOptions.NoInlining)]
        public void Orc_SelectionManagement_HasNoBreakingChanges()
        {
            var assembly = typeof(SelectionManager<>).Assembly;

            PublicApiApprover.ApprovePublicApi(assembly);
        }
    }
}
