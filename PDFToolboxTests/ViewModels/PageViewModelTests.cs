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
    public class PageViewModelTests
    {
        
        [TestClass()]
        public class PageViewModelTest
        {
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassedThrowsArgumentNullException()
            {
                PageViewModel vm = new PageViewModel(null);
            }
        }

        [TestClass()]
        public class SetPageTest
        {
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassedThrowsArgumentNullException()
            {
                PageViewModel vm = PageViewModelTests.GenerateGenericPageViewModel();
                vm.SetPage(null);
            }
        }

        [TestClass()]
        public class CopyTest
        {
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassedToGenericViewModelThrowsArgumentNullException()
            {
                PageViewModel vm = PageViewModelTests.GenerateGenericPageViewModel();
                vm.Copy(null);
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassedToEmptyViewModelThrowsArgumentNullException()
            {
                PageViewModel vm = PageViewModelTests.GenerateGenericBlankPageViewModel();
                vm.Copy(null);
            }
            [TestMethod()]
            public void GenericViewModelPassedToGenericViewModelEquates()
            {
                PageViewModel vm = PageViewModelTests.GenerateGenericBlankPageViewModel();
                vm.Copy(PageViewModelTests.GenerateGenericBlankPageViewModel());
                //assert equality
            }
        }

        [TestMethod()]
        public void MakeCopyTest()
        {
            throw new NotImplementedException();
        }

        [TestClass()]
        public class EqualsTest
        {
            [TestMethod()]
            public void NullEqualsGeneralReturnFalse()
            {
                PageViewModel vm = PageViewModelTests.GenerateGenericPageViewModel();

                Assert.IsFalse(vm.Equals(null));
            }
            [TestMethod()]
            public void EmptyEqualsGeneralReturnFalse()
            {
                PageViewModel vm = PageViewModelTests.GenerateGenericPageViewModel();
                PageViewModel emptyVM = PageViewModelTests.GenerateGenericBlankPageViewModel();

                Assert.IsFalse(vm.Equals(emptyVM));
            }
            [TestMethod()]
            public void GeneralEqualsGeneralReturnTrue()
            {
                PageViewModel vm = PageViewModelTests.GenerateGenericPageViewModel();
                PageViewModel vm2 = PageViewModelTests.GenerateGenericPageViewModel();

                Assert.IsTrue(vm.Equals(vm2));
            }
        }

        #region Helper Methods

        protected static Models.Page GenerateGenericPage()
        {
            return new Models.Page();
        }

        protected static PageViewModel GenerateGenericPageViewModel()
        {
            return new PageViewModel(GenerateGenericPage());
        }
        protected static PageViewModel GenerateGenericBlankPageViewModel()
        {
            return new PageViewModel();
        }

        #endregion
    }
}