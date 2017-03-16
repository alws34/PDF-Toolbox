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
        public class EqualsTests
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

        
        [TestClass()]
        public class CopyTests
        {
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassed_ThrowsArgumentNullException()
            {
                Page p = new Page();

                p.Copy(null);
            }
            [TestMethod()]
            public void BlankPassedEquates_ReturnsTrue()
            {
                Page p1 = new Page();
                Page p2 = new Page();

                p1.Copy(p2);

                Assert.AreEqual(p1, p2);
            }
            [TestMethod()]
            public void BasicPassedEquates_ReturnsTrue()
            {
                Page p1 = new Page();
                Page p2 = GenerateBasicPage1();

                p1.Copy(p2);

                Assert.AreEqual(p1, p2);
            }
        }

        [TestClass()]
        public class MakeCopyTests
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
            [TestMethod()]
            public void BasicPassedEquals_ReturnsTrue()
            {
                Page p1 = GenerateBasicPage1();
                Page p2 = Page.MakeCopy(p1);

                Assert.AreEqual(p1, p2);
            }
        }

        #region Helper Methods

        protected static Page GenerateBasicPage1()
        {
            Page p = new Page();

            p.fName = "\\\\test\\path";

            return p;
        }
        #endregion

    }
}