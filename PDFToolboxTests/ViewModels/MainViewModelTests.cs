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
            public void BlankBlankPassed_ThrowsArgumentNullException()
            {
                MainViewModel m = GenerateBlankMainViewModel();
                PageViewModel p = GenerateBlankPageViewModel();

                m.AddPage(null, p);
            }
        }

        [TestMethod()]
        public void CacheDocumentsTests()
        {

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
    }
}