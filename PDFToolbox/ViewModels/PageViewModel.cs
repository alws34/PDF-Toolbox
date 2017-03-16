using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media.Imaging;
using System.IO;

using PDFToolbox.Common.ViewModels;

namespace PDFToolbox.ViewModels
{
    public class PageViewModel : ViewModelBase
    {
        private Models.Page _page = null;

        private double _scale;
        public double Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                OnPropertyChanged("Scale");
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public PageViewModel(Models.Page page)
        {
            if (page == null) throw new ArgumentNullException("page");

            SetPage(page);
        }

        public PageViewModel()
        {
        }
        public void SetPage(Models.Page page)
        {
            if (page == null) throw new ArgumentNullException("page");

            _page = page;
            _page.uiStrings = new ObservableCollection<Common.UIString>();
            _page.uiStrings.CollectionChanged += OnStringsChanged;
        }

        public void Copy(PageViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException("page");

            if (vm._page == null)
            {
                _page = null;
                return;
            }

            if (_page==null)
            {
                _page = new Models.Page();
            }

            _page.Copy(vm._page);
            // or...?: _page = Models.Page.MakeCopy(vm._page);

        }
        public static PageViewModel MakeCopy(PageViewModel page)
        {
            if (page == null) throw new ArgumentNullException("PageViewModel page");

            PageViewModel vm = new PageViewModel();

            vm.Copy(page);

            return vm;
        }
        
        // override object.Equals
        public override bool Equals(object obj)
        {
            PageViewModel vm;
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            vm = (PageViewModel)obj;
            if (((_page==null && vm._page==null) || _page.Equals(vm._page)) &&
               this.Scale == vm.Scale &&
               this.IsSelected == vm.IsSelected)
            {
                return true;
            }
            
            return base.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new NotImplementedException();
            return base.GetHashCode();
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

        public string DocName
        {
            get { return _page.fName; }
            set
            {
                _page.fName = value;
                OnPropertyChanged("DocName");
            }
        }

        public int Number
        {
            get { return _page.number; }
            set
            {
                _page.number = value;
                OnPropertyChanged("Number");
            }
        }

        public float Rotation
        {
            get { return _page.rotation/*.FloatValue*/; }
            set
            {
                //_page.SetRotation(value);
                _page.rotation = value;
                OnPropertyChanged("Rotation");
            }
        }

        // FIXME: find a better way to handle 2 rotations. Maybe find a way to reduce it down to 1 again...
        public float FlatRotation
        {
            //get { return _page.rotation.FloatValue + _page.originalRotation.FloatValue; }
            get { return _page.rotation + _page.originalRotation; }
            /*set
            {
                _page.SetRotation(value);
                OnPropertyChanged("Rotation");
            }*/
        }

        // Delete?
        /*public bool IsImagePreview
        {
            get { return _page.isImagePreview; }
            set
            {
                _page.isImagePreview = value;
                OnPropertyChanged("IsImagePreview");
            }
        }*/

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

        public void OnStringsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Notify that Count may have been changed
            OnPropertyChanged("StringsCount");


        }
    }
}
