using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDFToolbox.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFToolbox.ViewModels.Tests
{
    [TestClass()]
    public class MainViewModelTests
    {

        [TestMethod()]
        public void CachePagesTests()
        {

        }

        [TestClass()]
        public class AddPageTests
        {
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullNullPassed_ThrowsArgumentNullException()
            {
                MainViewModel m = GenerateBlankMainViewModel();

                m.AddPage(null, null);
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void BlankNullPassed_ThrowsArgumentNullException()
            {
                MainViewModel m = GenerateBlankMainViewModel();
                DocumentViewModel d = GenerateBlankDocumentViewModel();

                m.AddPage(d, null);
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullBlankPassed_ThrowsArgumentNullException()
            {
                MainViewModel m = GenerateBlankMainViewModel();
                PageViewModel p = GenerateBlankPageViewModel();

                m.AddPage(null, p);
            }
            [TestMethod()]
            public void BlankBlankPassedEquals_ReturnTrue()
            {
                MainViewModel m = GenerateBlankMainViewModel();
                DocumentViewModel d = GenerateBlankDocumentViewModel();
                PageViewModel p = GenerateBlankPageViewModel();

                m.AddPage(d, p);

                Assert.AreEqual(p, d.Pages[0]);
            }
        }

        [TestClass()]
        public class CacheDocumentsTests
        {
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassed_ThrowsArgumentNullException()
            {
                MainViewModel m = GenerateBlankMainViewModel();

                m.CacheDocuments(null);
            }

            [TestMethod()]
            [ExpectedException(typeof(ArgumentException))]
            public void ZeroLenArrayPassed_ThrowsArgumentNullException()
            {
                MainViewModel m = GenerateBlankMainViewModel();
                Models.Document[] a = GenerateZeroLengthDocumentArray();

                m.CacheDocuments(a);
            }
            [TestMethod()]
            public void DocumentsArrayFilledWithNullsPassed_DocumentsCountEqualsZero_ReturnsTrue()
            {
                MainViewModel m = GenerateBlankMainViewModel();
                Models.Document[] a = GenerateFiveLengthDocumentArrayFilledWithNulls();

                m.CacheDocuments(a);

                Assert.IsTrue(m.Documents.Count == 0);
            }
            [TestMethod()]
            public void DocumentsArrayPopulatedTwoDocumentsAndRestNullsPassed_DocumentsCountEqualsTwo_ReturnsTrue()
            {
                MainViewModel m = GenerateBlankMainViewModel();
                Models.Document[] a = GenerateFiveLengthDocumentArrayFilledWithNulls();
                a[1] = new Models.Document();
                a[3] = new Models.Document();

                m.CacheDocuments(a);

                Assert.IsTrue(m.Documents.Count == 2);
            }
        }
        
        [TestClass()]
        public class AddDocumentsTests
        {
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassed_ThrowsArgumentNullException()
            {
                MainViewModel m = GenerateBlankMainViewModel();

                m.AddDocuments(null);
            }

            [TestMethod()]
            [ExpectedException(typeof(ArgumentException))]
            public void ZeroLenArrayPassed_ThrowsArgumentNullException()
            {
                MainViewModel m = GenerateBlankMainViewModel();
                Models.Document[] a = GenerateZeroLengthDocumentArray();

                m.AddDocuments(a);
            }
            [TestMethod()]
            public void DocumentsArrayFilledWithNullsPassed_DocumentsCountEqualsZero_ReturnsTrue()
            {
                MainViewModel m = GenerateBlankMainViewModel();
                Models.Document[] a = GenerateFiveLengthDocumentArrayFilledWithNulls();

                m.AddDocuments(a);

                Assert.IsTrue(m.Documents.Count == 0);
            }
            [TestMethod()]
            public void DocumentsArrayPopulatedTwoDocumentsAndRestNullsPassed_DocumentsCountEqualsTwo_ReturnsTrue()
            {
                MainViewModel m = GenerateBlankMainViewModel();
                Models.Document[] a = GenerateFiveLengthDocumentArrayFilledWithNulls();
                a[1] = new Models.Document();
                a[3] = new Models.Document();

                m.AddDocuments(a);

                Assert.IsTrue(m.Documents.Count == 2);
            }
        }

        [TestMethod()]
        public void CopyDocumentToTests()
        {

        }

        [TestMethod()]
        public void MovePageTests()
        {

        }

        [TestMethod()]
        public void MoveDocumentTests()
        {

        }

        [TestMethod()]
        public void MovePageToDocTests()
        {

        }

        [TestMethod()]
        public void SplitDocumentTests()
        {

        }

        [TestMethod()]
        public void IsValidDropItemTests()
        {

        }

        public static MainViewModel GenerateBlankMainViewModel()
        {
            return new MainViewModel();
        }

        public static DocumentViewModel GenerateBlankDocumentViewModel()
        {
            return new DocumentViewModel(new Models.Document());
        }
        public static PageViewModel GenerateBlankPageViewModel()
        {
            return new PageViewModel(new Models.Page());
        }

        public static Models.Document[] GenerateZeroLengthDocumentArray()
        {
            return new Models.Document[0];
        }

        public static Models.Document[] GenerateFiveLengthDocumentArrayFilledWithNulls()
        {
            return new Models.Document[5];

            
        }
    }
}