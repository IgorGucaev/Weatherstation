using Station.Tests.Signals.Domain.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Station.Tests.Signals.Domain
{
    [TestClass]
    public class SignalTest : DomainTest
    {
        public SignalTest()
            :base()
        {

        }

        [TestMethod]
        public void TestMethod1()
        {
            var Signal = _services.SignalService.Get("5a31241b1f87770878ded71e");
        }
    }
}