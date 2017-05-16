using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media.Imaging;
using System.IO;

using PDFToolbox.Behaviors;
using PDFToolbox.Models;

namespace PDFToolbox.ViewModels
{
    public class PageViewModel : ElementViewModel, IDragable, IDropable
    {
        public PageViewModel(Page page) : base(page)
        { }
        
        public int PageNumber
        {
            get { return _page.number; }
            set
            {
                _page.number = value;
                OnPropertyChanged("PageNumber");
            }
        }

        public Type DataType
        {
            get
            {
                return typeof(ElementViewModel);
            }
        }

        public void OnStringsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Notify that Count may have been changed
            OnPropertyChanged("Strings");


        }
        
        public void Remove(object i)
        {
            PagesViewModel.Instance.Remove(this);
        }

        void IDropable.Drop(object data, int index)
        {
            PagesViewModel.Instance.Drop(data, index);
        }
        
    }
}
