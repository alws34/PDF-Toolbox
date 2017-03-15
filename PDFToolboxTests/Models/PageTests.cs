using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDFToolbox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace PDFToolbox.Models.Tests
{
    [TestClass()]
    public class PageTests
    {
        [TestClass()]
        public class EqualsTest
        {
            [TestMethod()]
            public void NullEqualsGeneral_ReturnFalse()
            {
                Page p = new Page();

                Assert.IsFalse(p.Equals(null));
            }
            [TestMethod()]
            public void GeneralEqualsGeneral_ReturnTrue()
            {
                Page p1 = new Page();
                Page p2 = new Page();

                Assert.IsTrue(p1.Equals(p2));
            }
        }

        /*
        [TestMethod()]
        public void CopyTest()
        {
            Assert.Fail();
        }*/

        [TestClass()]
        public class MakeCopyTest
        {
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassed_ThrowsArgumentNullException()
            {
                Page.MakeCopy(null);
            }

            [TestMethod()]
            public void BlankPassedEquals_ReturnsTrue()
            {
                Page p1 = new Page();
                Page p2 = Page.MakeCopy(p1);

                Assert.AreEqual(p1, p2);
            }
        }

        protected Page GenerateTestPage1()
        {
            Page p = new Page();

            p.image = new BitmapImage();
            p.imageStream.Write(new byte[] { 32, 32, 32, 32, 32 }, 0, 5);
            p.rotation = 0f;

            return p;
        }
        
    }
}