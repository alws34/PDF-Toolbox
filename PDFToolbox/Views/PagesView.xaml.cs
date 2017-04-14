using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using PDFToolbox.Common.ViewModels;
using PDFToolbox.ViewModels;
using PDFToolbox.IO;

namespace PDFToolbox.Views
{
    /// <summary>
    /// Interaction logic for PagesView.xaml
    /// </summary>
    public partial class PagesView : UserControl
    {
        #region Dependency Properties
        public static readonly DependencyProperty DocumentProperty = RegisterProperty<DocumentViewModel>("Document");
        public static readonly DependencyProperty ZoomProperty = RegisterProperty<Double>("Zoom");

        public DocumentViewModel Document
        {
            get { return (DocumentViewModel)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }
        public Double Zoom
        {
            get { return (Double)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }
        #endregion
        #region Event Handling
        // switch these with Behaviors...?
        public event EventHandler<DragEventArgs> ObjectDropped;
        public event EventHandler DragEntered;

        protected virtual void OnObjectDropped(object sender, DragEventArgs e)
        {
            ObjectDropped?.Invoke(sender, e);
        }
        protected virtual void OnDragEntered(EventArgs e)
        {
            DragEntered?.Invoke(this, e);
        }
        #endregion

        public PagesView()
        {
            InitializeComponent();
        }


        private void lbxPages_Drop(object sender, DragEventArgs e)
        {
            //TODO: Need to copy from MainWindow.xaml.cs.  Or do it right this time.
            OnObjectDropped(sender, e);
            //OnObjectDropped(sender, e);
        }
        private void lbxPages_DragEnter(object sender, DragEventArgs e)
        {
            //TODO: Need to copy from MainWindow.xaml.cs.  Or do it right this time.
        }

        private void lbxPages_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //TODO: Need to copy from MainWindow.xaml.cs.  Or do it right this time.
        }

        private void lbxPages_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            //TODO: Need to copy from MainWindow.xaml.cs.  Or do it right this time.
        }

        private void OnObjectDroppedd(object sender, DragEventArgs e)
        {
            HitTestResult hit = VisualTreeHelper.HitTest(sender as ListBox, e.GetPosition(sender as ListBox));
            PageViewModel draggedPage = e.Data.GetData(typeof(PageViewModel)) as PageViewModel;

            // DraggedItem is a pageDict -> rearrange
            if (draggedPage != null)
            {
                ListBoxItem lbxItemDropTarget = Toolbox.FindParent<ListBoxItem>(hit.VisualHit);
                PageViewModel targetPage;
                int sourceIndex = Document.GetPageIndex(draggedPage);
                int targetIndex = Document.Pages.Count - 1;

                // Move pageDict to last element if dropped on blank-space
                if (lbxItemDropTarget != null)
                {
                    targetPage = lbxItemDropTarget.DataContext as PageViewModel;
                    targetIndex = Document.GetPageIndex(targetPage);
                }
                Document.Pages.Move(sourceIndex, targetIndex);

                return;
            }

            // Get any files dropped onto pageview
            Models.Document[] dropFiles = FileIO.ExtractDocument(e.Data);

            if(Document==null)
            {
                Document = new DocumentViewModel(new Models.Document());
            }

            // If any files dropped, load their pages
            if (dropFiles != null && dropFiles.Length > 0)
            {
                for (int i = 0; i < dropFiles.Length; i++)
                {
                    Document.AddPages(dropFiles[i].pages.ToArray());
                }
                return;
            }
        }



        private static DependencyProperty RegisterProperty<T>(string varName, T defaultValue = default(T))
        {
            return DependencyProperty.Register(varName, typeof(T), typeof(PagesView), new PropertyMetadata(defaultValue));
        }
    }
}
