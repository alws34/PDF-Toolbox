using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Threading.Tasks;

namespace PDFToolbox.ViewModels
{
    public class DocumentViewModel : Common.ViewModels.ViewModelBase
    {
        private Models.Document _doc = null;
        public ObservableCollection<PageViewModel> Pages { get; private set; }

        public DocumentViewModel(Models.Document document)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            _doc = document;

            Pages = new ObservableCollection<PageViewModel>(_doc.pages);
            Pages.CollectionChanged += OnPagesChanged;
        }

        #region Properties
        public BitmapImage Image
        {
            get { return _doc.image; }
            set
            {
                _doc.image = value;
                OnPropertyChanged("Image");
            }
        }

        public string DocName
        {
            get { return _doc.fName; }
            set
            {
                _doc.fName = value;
                OnPropertyChanged("DocName");
            }
        }

        public int PageCount
        {
            get { return Pages.Count; }
        }

        public float Rotation
        {
            get { return (Pages != null && Pages.Count > 0 ? Pages[0].Rotation : 0f); }
            /*set
            {
                
                OnPropertyChanged("Rotation");
            }*/
        }
        #endregion

        #region Utils

        public void AddPage(PageViewModel page)
        {
            if (page == null) throw new ArgumentNullException("PageViewModel page");

            Pages.Add(page);
        }
        public void AddPages(PageViewModel[] pages)
        {
            if (pages == null) throw new ArgumentNullException("PageViewModel page");
            if (pages.Length == 0) throw new ArgumentException("Array param length is zero.", "PageViewModel[] pages");

            foreach (PageViewModel p in pages)
            {
                if (p == null) continue;

                Pages.Add(p);
            }
        }

        private void OnPagesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //FIXME: change this to only fire when the count actually changes (on add/remove page)
            // Notify that Count may have been changed
            OnPropertyChanged("PageCount");

            if (0 < Pages.Count)
                Image = Pages[0].Image;
            else
                Image = null;
        }

        public int GetPageIndex(PageViewModel page)
        {
            return Pages.IndexOf(Pages.Where(p => p.ID == page.ID).FirstOrDefault());
        }
        
        #endregion
    }
}
