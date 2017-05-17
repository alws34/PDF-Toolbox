using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text;

using PDFToolbox.IO;
using PDFToolbox.Behaviors;

namespace PDFToolbox.ViewModels
{
    public class PagesViewModel : Common.ViewModels.ViewModelBase, IDropable
    {
        //HACK: using singleton for IDropable.Remove functionality.  Please find a better way and remove
        private static PagesViewModel _self = null;
        
        #region Properties
        public static PagesViewModel Instance
        {
            get
            {
                if (_self == null)
                    _self = new PagesViewModel();
                return _self;
            }
        }

        private DocumentViewModel _viewingDoc = null;
        public DocumentViewModel ViewingDocument
        {
            get { return _viewingDoc; }
            set
            {
                _viewingDoc = value;
                OnPropertyChanged("SelectedDocument");
            }
        }
        
        public ObservableCollection<PageViewModel> Pages
        {
            get
            {
                if (ViewingDocument!=null)
                    return ViewingDocument.Pages;
                return null;
            }
        }

        Type IDropable.DataType
        {
            get
            {
                return typeof(ElementViewModel);
            }
        }

        public Type DataType
        {
            get
            {
                return ((IDragable)Instance).DataType;
            }
        }
        #endregion

        public void OnObjectDroppedInPages(object sender, DragEventArgs e)
        {
            HitTestResult hit = VisualTreeHelper.HitTest(sender as ListBox, e.GetPosition(sender as ListBox));

            // DraggedItem is a pageDict -> rearrange
            /*if (e.Data.GetDataPresent(typeof(PageViewModel)))
            {

                PageViewModel draggedPage = e.Data.GetData(typeof(PageViewModel)) as PageViewModel;
                ListBoxItem lbxItemDropTarget = Toolbox.FindParent<ListBoxItem>(hit.VisualHit);
                PageViewModel targetPage;
                int sourceIndex = ViewingDocument.GetPageIndex(draggedPage);
                int targetIndex = ViewingDocument.Pages.Count - 1;

                // Move pageDict to last element if dropped on blank-space
                if (lbxItemDropTarget != null)
                {
                    targetPage = lbxItemDropTarget.DataContext as PageViewModel;
                    targetIndex = ViewingDocument.GetPageIndex(targetPage);
                }
                ViewingDocument.Pages.Move(sourceIndex, targetIndex);

                return;
            }*/

            // Get any files dropped onto pageview
            
            AddDroppedPages(FileIO.ExtractDocument(e.Data));
        }


        public void AddPage(DocumentViewModel document, PageViewModel page)
        {
            if (document == null) throw new ArgumentNullException("document");
            if (page == null) throw new ArgumentNullException("page");

            document.AddPage(page);
        }
        private void AddPages(DocumentViewModel document, PageViewModel[] pages)
        {
            if (document == null) throw new ArgumentNullException("document");
            if (pages == null) throw new ArgumentNullException("pages");

            for (int i = 0; i < pages.Length; i++)
            {
                AddPage(document, pages[i]);
            }
        }

        private void AddDroppedPages(Models.Document[] dropFiles)
        {
            // Don't make a fuss if this is null...
            if (dropFiles == null) return;

            if (ViewingDocument == null)
            {
                ViewingDocument = new DocumentViewModel(new Models.Document());
            }

            // If any files dropped, load their pages
            if (dropFiles != null && dropFiles.Length > 0)
            {
                for (int i = 0; i < dropFiles.Length; i++)
                {
                    AddPages(ViewingDocument, dropFiles[i].pages.ToArray());
                }
            }
        }

        public void Drop(object data, int index)
        {
            ElementViewModel item = (ElementViewModel)data;
            if (item == null) return;

            if(ViewingDocument.Pages.Where(i => i.ID == item.ID).Count() == 0)
            {
                ViewingDocument.Pages.Insert(index, (PageViewModel)item);
                
            }
        }

        public void Remove(object data)
        {
            if (data.GetType().BaseType == typeof(ElementViewModel))
            {
                ViewingDocument.Pages.Remove((PageViewModel)data);
            }
        }
    }
}
