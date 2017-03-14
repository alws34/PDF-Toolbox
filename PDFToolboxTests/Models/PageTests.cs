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
        /*[TestMethod()]
        public void PageTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CopyTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MakeCopyTest()
        {
            Assert.Fail();
        }*/

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