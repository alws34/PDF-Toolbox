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
        public class CopyTest
        {
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassedToGenericViewModel_ThrowsArgumentNullException()
            {
                PageViewModel vm = GenerateGenericPageViewModel();
                vm.Copy(null);
            }
            [TestMethod()]
            [ExpectedException(typeof(ArgumentNullException))]
            public void NullPassedToEmptyViewModel_ThrowsArgumentNullException()
            {
                PageViewModel vm = GenerateGenericBlankPageViewModel();

                vm.Copy(null);
            }
            [TestMethod()]
            public void GenericViewModelPassedToGenericViewModel_ReturnsTrue()
            {
                PageViewModel vm1 = GenerateGenericBlankPageViewModel();
                PageViewModel vm2 = GenerateGenericBlankPageViewModel();

                vm1.Copy(vm2);
                
                Assert.AreEqual(vm1, vm2);
            }
        }
        
        [TestClass()]
        public class EqualsTest
        {
            [TestMethod()]
            public void NullEqualsGeneral_ReturnFalse()
            {
                PageViewModel vm = GenerateGenericPageViewModel();

                Assert.IsFalse(vm.Equals(null));
            }
            [TestMethod()]
            public void EmptyEqualsGeneral_ReturnFalse()
            {
                PageViewModel vm = GenerateGenericPageViewModel();
                PageViewModel emptyVM = GenerateGenericBlankPageViewModel();

                Assert.IsFalse(vm.Equals(emptyVM));
            }
            [TestMethod()]
            public void GeneralEqualsGeneral_ReturnTrue()
            {
                PageViewModel vm1 = GenerateGenericPageViewModel();
                PageViewModel vm2 = GenerateGenericPageViewModel();

                Assert.IsTrue(vm1.Equals(vm2));
            }
        }

        #region Helper Methods

        protected static Models.Page GenerateGenericPage()
        {
            return new Models.Page();
        }
        protected static Models.Page GenerateBasicPage()
        {
            Models.Page p = new Models.Page();
            p.fName = "\\\\test\\path";
            return p;
        }

        protected static PageViewModel GenerateGenericPageViewModel()
        {
            return new PageViewModel(GenerateGenericPage());
        }
        protected static PageViewModel GenerateGenericBlankPageViewModel()
        {
            return new PageViewModel(new Models.Page());
        }

        #endregion
    }
}