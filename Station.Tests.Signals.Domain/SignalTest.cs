using Station.Tests.Signals.Domain.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Station.Tests.Signals.Domain
{
    [TestClass]
    public class SignalTest : DomainTest
    {

        [TestMethod]
        public void TestMethod1()
        {
            var Signal = _services.SignalService.Get(121212);
        }
    }
}