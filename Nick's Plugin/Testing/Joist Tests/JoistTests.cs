using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nick_s_Plugin;
using NUnit.Framework;

namespace CADTesting
{
    [TestFixture, Apartment(ApartmentState.STA)]
    class JoistTests
    {
        [Test]
        public void Default_Spacing()
        {
            Assert.That(new Joists().JoistSpacing == 16);

            
        }

        [Test]
        public void Get_Spacing_From_User()
        {
            Assert.That(new Joists().JoistSpacing == 16);


        }
    }
}
