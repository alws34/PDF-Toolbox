using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

using PDFToolbox.Common.ViewModels;
using PDFToolbox.Models;

namespace PDFToolbox.ViewModels
{
    public class ElementViewModel : ViewModelBase
    {
        protected Page _page = null;
        
        private bool _isSelected;
        private double _scale;

        #region Properties
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public double Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                OnPropertyChanged("Scale");
            }
        }

        public Stream ImageStream
        {
            get { return _page.imageStream; }
            set
            {
                _page.imageStream = value;
                OnPropertyChanged("ImageStream");
            }
        }

        public ObservableCollection<Common.UIString> Strings
        {
            get { return _page.uiStrings; }
            private set
            {
                _page.uiStrings = value;
                OnPropertyChanged("Strings");
            }
        }

        public int ID
        {
            get { return _page.id; }
        }

        public BitmapImage Image
        {
            get { return _page.image; }
            set
            {
                _page.image = value;
                OnPropertyChanged("Image");
            }
        }

        public string FilePath
        {
            get { return _page.fName; }
            set
            {
                _page.fName = value;
                OnPropertyChanged("FilePath");
            }
        }

        /*public int Number
        {
            get { return _page.number; }
            set
            {
                _page.number = value;
                OnPropertyChanged("Number");
            }
        }*/

        public float Rotation
        {
            get { return _page.rotation; }
            set
            {
                _page.rotation = value;
                OnPropertyChanged("Rotation");
            }
        }
        #endregion

        public ElementViewModel(Page page)
        {
            if (page == null) throw new ArgumentNullException("page");

            _page = page;
        }
        // Copy constructor
        public ElementViewModel(ElementViewModel e)
        {
            if (e == null) throw new ArgumentNullException("e");

            Copy(e);
        }

        public override int GetHashCode()
        {
            return _page.GetHashCode();
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            ElementViewModel vm;
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            vm = (ElementViewModel)obj;
            if (((_page == null && vm._page == null) || _page.Equals(vm._page)) &&
               Scale == vm.Scale &&
               IsSelected == vm.IsSelected)
            {
                return true;
            }

            return base.Equals(obj);
        }


        public void Copy(ElementViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException("page");

            if (vm._page == null)
            {
                _page = null;
                return;
            }

            if (_page == null)
            {
                _page = new Page();
            }

            _page.Copy(vm._page);

        }
    }
}
