using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Unit.Test
{
    [TestFixture]
    public class TestClass
    {
        public Ipod_Inventory.Order myNewOrder = null;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            myNewOrder = new Ipod_Inventory.Order();
            myNewOrder.Brazil = new Ipod_Inventory.Inventory("Brazil", 100, 100);
            myNewOrder.Argentina = new Ipod_Inventory.Inventory("Argentina", 100, 50);
        }

        [TestCase]
        public void BrazilTest()
        {
            
            myNewOrder.New(80, "Brazil");
            int result = myNewOrder.GenerateBill();
            Assert.AreEqual(7200, result);
        }

        [TestCase]
        public void ArgentinaTest()
        {
            
            myNewOrder.New(120, "Argentina");
            int result = myNewOrder.GenerateBill();
            Assert.AreEqual(7800, result);
        }

        [TestCase]
        public void FailedTest()
        {

            myNewOrder.New(220, "Argentina");
            int result = myNewOrder.GenerateBill();
            Assert.AreEqual(7800, result);
        }
    }
}
