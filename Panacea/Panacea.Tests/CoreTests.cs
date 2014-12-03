using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Panacea.Tests
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void TestShuffle()
        {
            Core core = new Core();
            
            // Get 37 letters
            string randLetters = "";
            for (int i = 0; i < 37; i++)
            {
                char letter = core.ShuffleBag.Next();
                randLetters += letter;
            }

            Debug.WriteLine(randLetters.ToString());
        }
    }
}
