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
            public void NullPassed_ThrowsArgumentNullException()
            {
                PageViewModel vm = new PageViewModel(null);
            }
        }

        [TestClass()]
        public class SetPageTest
        {
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassed_ThrowsArgumentNullException()
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
            public void NullPassedToGenericViewModel_ThrowsArgumentNullException()
            {
                PageViewModel vm = PageViewModelTests.GenerateGenericPageViewModel();
                vm.Copy(null);
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassedToEmptyViewModel_ThrowsArgumentNullException()
            {
                PageViewModel vm = PageViewModelTests.GenerateGenericBlankPageViewModel();

                vm.Copy(null);
            }
            [TestMethod()]
            public void GenericViewModelPassedToGenericViewModel_ReturnsTrue()
            {
                PageViewModel vm1 = PageViewModelTests.GenerateGenericBlankPageViewModel();
                PageViewModel vm2 = PageViewModelTests.GenerateGenericBlankPageViewModel();

                vm1.Copy(vm2);

                Assert.IsTrue(vm1.Equals(vm2));
            }
        }

        [TestClass()]
        public class MakeCopyTest
        {
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassed_ThrowsArgumentNullException()
            {
                PageViewModel.MakeCopy(null);
            }

            [TestMethod()]
            public void BlankPassedEquates_AssertsTrue()
            {
                PageViewModel vm1 = PageViewModelTests.GenerateGenericBlankPageViewModel();
                PageViewModel vm2;

                vm2 = PageViewModel.MakeCopy(vm1);

                Assert.IsTrue(vm1.Equals(vm2));
            }
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
                PageViewModel vm1 = PageViewModelTests.GenerateGenericPageViewModel();
                PageViewModel vm2 = PageViewModelTests.GenerateGenericPageViewModel();

                Assert.IsTrue(vm1.Equals(vm2));
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