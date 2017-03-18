using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDFToolbox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFToolbox.Models.Tests
{
    [TestClass()]
    public class DocumentTests
    {
        [TestClass()]
        public class RenameTests
        {
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassed_ThrowsArgumentNullException()
            {
                Document d = GenerateBlankDocument();

                d.Rename(null);
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void BlankStringPassed_ThrowsArgumentNullException()
            {
                Document d = GenerateBlankDocument();

                d.Rename("");
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void SpacesPassed_ThrowsArgumentNullException()
            {
                Document d = GenerateBlankDocument();

                d.Rename("  ");
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void TabsPassed_ThrowsArgumentNullException()
            {
                Document d = GenerateBlankDocument();

                d.Rename("      ");
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NewLinePassed_ThrowsArgumentNullException()
            {
                Document d = GenerateBlankDocument();

                d.Rename(Environment.NewLine);
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void WhitespacePassed_ThrowsArgumentNullException()
            {
                Document d = GenerateBlankDocument();

                d.Rename(WhiteSpace());
            }
            [TestMethod()]
            public void ValidStringPassedEquates_ReturnsTrue()
            {
                Document d = GenerateBlankDocument();
                string oldName = d.fName;
                string newName = "testingName";

                d.Rename(newName);

                // Note: if d.fName == newName, then since oldName != newName...
                //       d.fName cannot equal d.oldName at this point
                //if (d.fName == oldName) Assert.Fail(); <-- Not needed

                Assert.AreEqual(d.fName, newName);
            }
        }




        protected static Document GenerateBlankDocument()
        {
            return new Document();
        }

        protected static string WhiteSpace()
        {
            return "  " + "     " + Environment.NewLine;
        }
    }
}