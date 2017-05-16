using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;

using PDFToolbox.IO;
using PDFToolbox.Common.ViewModels;


namespace PDFToolbox.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Boilerplate
        #region Event declarations
        private ICommand _addDoc = null;
        public ICommand AddDoc
        {
            get { return _addDoc; }
        }

        private ICommand _saveDoc = null;
        public ICommand SaveDoc
        {
            get { return _saveDoc; }
        }

        private ICommand _saveAllDocs = null;
        public ICommand SaveAllDocs
        {
            get { return _saveAllDocs; }
        }
        private ICommand _rotPageCW90 = null;
        public ICommand RotPageCW90
        {
            get { return _rotPageCW90; }
        }
        private ICommand _rotPageCCW90 = null;
        public ICommand RotPageCCW90
        {
            get { return _rotPageCCW90; }
        }
        private ICommand _splitDoc = null;
        public ICommand SplitDoc
        {
            get { return _splitDoc; }
        }


        private ICommand _clearDocs = null;
        public ICommand ClearDocs
        {
            get { return _clearDocs; }
        }

        private ICommand _removePage = null;
        public ICommand RemovePage
        {
            get { return _removePage; }
        }

        private ICommand _removeDoc = null;
        public ICommand RemoveDoc
        {
            get { return _removeDoc; }
        }

        private ICommand _clearCanvas = null;
        public ICommand ClearCanvas
        {
            get { return _clearCanvas; }
        }

        private ICommand _leftMouseUpCanvas = null;
        public ICommand LeftMouseUpCanvas
        {
            get { return _leftMouseUpCanvas; }
        }

        #endregion
        #region Key Bindings
        private ICommand _keyDeletePressed = null;
        public ICommand KeyDeletePressed
        {
            get { return _keyDeletePressed; }
        }
        #endregion
        
        #region Properties
        private ObservableCollection<DocumentViewModel> _docs = new ObservableCollection<DocumentViewModel>();
        public ObservableCollection<DocumentViewModel> Documents
        {
            get { return _docs; }
        }

        public ObservableCollection<PageViewModel> Pages
        {
            get
            {
                if (HaveSelectedDoc)
                    return SelectedDocument.Pages;
                return null;
            }
        }

        private DocumentViewModel _selectedDoc = null;
        public DocumentViewModel SelectedDocument
        {
            get { return _selectedDoc; }
            set
            {
                _selectedDoc = value;
                OnPropertyChanged("SelectedDocument");
                HaveSelectedDoc = (_selectedDoc != null);
            }
        }

        private PageViewModel _selectedPage = null;
        public PageViewModel SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                _selectedPage = value;
                OnPropertyChanged("SelectedPage");
                HaveSelectedPage = (_selectedPage != null);
                
            }
        }

        private ObservableCollection<PageViewModel> _selectedPages = null;
        public ObservableCollection<PageViewModel> SelectedPages
        {
            get { return _selectedPages; }
            set
            {
                _selectedPages = value;
                OnPropertyChanged("SelectedPages");
                HaveSelectedPage = (_selectedPages != null && _selectedPages.Count > 0);
            }
        }

        private bool _haveSelectedDoc = false;
        public bool HaveSelectedDoc
        {
            get { return _haveSelectedDoc; }
            set
            {
                _haveSelectedDoc = value;
                OnPropertyChanged("HaveSelectedDoc");
            }
        }

        private bool _haveSelectedPage = false;
        public bool HaveSelectedPage
        {
            get { return _haveSelectedPage; }
            set
            {
                _haveSelectedPage = value;
                OnPropertyChanged("HaveSelectedPage");
            }
        }

        private bool _dragOverPageViewValid = false;
        public bool DragOverPageViewValid
        {
            get { return _dragOverPageViewValid; }
            set
            {
                _dragOverPageViewValid = value;
                OnPropertyChanged("DragOverPageViewValid");
                //DragOverPageViewVisible = (value ? Visibility.Visible : Visibility.Hidden);
            }
        }

        private bool _dragOverPageViewVisible = false;
        public Visibility DragOverPageViewVisible
        {
            get { return (_dragOverPageViewVisible? Visibility.Visible : Visibility.Hidden); }
            set
            {
                _dragOverPageViewVisible = (Visibility.Visible==value);
                OnPropertyChanged("DragOverPageViewVisible");
            }
        }

        private PageViewModel _editPage = null;
        public PageViewModel EditPage
        {
            get { return _editPage; }
            set
            {
                _editPage = value;
                OnPropertyChanged("EditPage");
                IsEditingPage = (_editPage != null);
            }
        }

        private bool _isEditingPage = false;
        public bool IsEditingPage
        {
            get { return _isEditingPage; }
            set
            {
                _isEditingPage = value;
                OnPropertyChanged("IsEditingPage");
            }
        }

        private string _textToAddToCanvas = "";
        public string TextToAddToCanvas
        {
            get { return _textToAddToCanvas; }
            set
            {
                _textToAddToCanvas = value;
                OnPropertyChanged("TextToAddToCanvas");
            }
        }

        private bool _toggleButtonAddText = false;
        public bool ToggleButtonAddText
        {
            get { return _toggleButtonAddText; }
            set
            {
                _toggleButtonAddText = value;
                OnPropertyChanged("ToggleButtonAddText");
            }
        }

        #endregion
        #endregion

        public readonly string[] SUPPORTED_FILE_TYPES = { ".PDF" };

        public MainViewModel()
        {

            //----[ Events ]----------------------------------------------------------------
            // General
            _addDoc = new Common.Commands.DelegateCommand(OnAddDoc);
            _saveDoc = new Common.Commands.DelegateCommand(OnSaveDoc);
            _saveAllDocs = new Common.Commands.DelegateCommand(OnSaveAllDocs);
            _rotPageCW90 = new Common.Commands.DelegateCommand(OnRotatePageCW90);
            _rotPageCCW90 = new Common.Commands.DelegateCommand(OnRotatePageCCW90);
            _splitDoc = new Common.Commands.DelegateCommand(OnSplitDoc);

            _clearDocs = new Common.Commands.DelegateCommand(OnClearDocs);
            _removePage = new Common.Commands.DelegateCommand(OnRemovePage);
            _removeDoc = new Common.Commands.DelegateCommand(OnRemoveDoc);

            // Canvas
            _clearCanvas = new Common.Commands.DelegateCommand(OnClearCanvas);
            _leftMouseUpCanvas = new Common.Commands.DelegateCommand(OnLeftMouseUpCanvas);

            // Key binding events
            _keyDeletePressed = new Common.Commands.DelegateCommand(OnKeyDeletePressed);

        }

        #region Event implimentations
        #region Commands
        private void OnAddDoc(object param)
        {
            AddNewDoc(new Models.Document()
            {
                fName = String.Format("Doc{0}", _docs.Count)
            });
        }

        private void OnSaveDoc(object param)
        {
            if (!HaveSelectedDoc)
                return;

            SaveDocument(SelectedDocument);
        }

        private void OnSaveAllDocs(object param)
        {
            if (Documents.Count==0)
                return;

            for (int i = 0; i < Documents.Count; i++)
            {
                SaveDocument(Documents[i]);
            }
        }

        private void OnRotatePageCW90(object param)
        {
            RotatePage(SelectedPage, 90f);
        }
        private void OnRotatePageCCW90(object param)
        {
            RotatePage(SelectedPage, -90f);
        }

        private void OnSplitDoc(object param)
        {
            //string value = Interaction.Inputbox
            //SplitDocument(SelectedDocument, 3);
            
        }


        private void OnClearDocs(object param)
        {
            Documents.Clear();
            SelectedDocument = null;

            IO.FileIO.Cleanup();
            //Toolbox.RemoveTemporaryFiles();
        }

        private void OnRemovePage(object param)
        {
            if (!HaveSelectedPage)
                return;

            int idx = SelectedDocument.GetPageIndex(SelectedPage);
            SelectedDocument.Pages.RemoveAt(idx);

            if (SelectedDocument.PageCount > 0)
            {
                Helpers.D.Log("MainViewModel::OnRemovePage: pageDict.count:{0}", SelectedDocument.PageCount);
                SelectedPage = SelectedDocument.Pages.ElementAt(Math.Min(Math.Max(0, SelectedDocument.Pages.Count - 1), idx));
            }
            else
                SelectedPage = null;
        }

        private void OnRemoveDoc(object param)
        {
            if (!HaveSelectedDoc)
                return;

            Documents.Remove(SelectedDocument);
        }

        private void OnKeyDeletePressed(object param)
        {
            if (!HaveSelectedPage)
                _removeDoc.Execute(param);
            else
                _removePage.Execute(param);
        }

        private void OnLeftMouseUpCanvas(object param)
        {
            /*Common.UIString str;
            MainWindow win;

            if (ToggleButtonAddText)
            {
                str = new Common.UIString();
                win = Toolbox.MainWindow as MainWindow;

                if(win!=null)
                {
                    str.String = win.AddTextTextBox.Text;
                    //str.X = win
                }
            }*/
        }

        private void OnClearCanvas(object param)
        {
            SelectedPage.Strings.Clear();
            // impliment and run ClearCanvas on SelectedPage
        }
        #endregion
        #region Delegated events
        #region Pages
        public void OnObjectDroppedInPages(object sender, DragEventArgs e)
        {
            HitTestResult hit = VisualTreeHelper.HitTest(sender as ListBox, e.GetPosition(sender as ListBox));

            // DraggedItem is a pageDict -> rearrange
            if (e.Data.GetDataPresent(typeof(PageViewModel)))
            {

                PageViewModel draggedPage = e.Data.GetData(typeof(PageViewModel)) as PageViewModel;
                ListBoxItem lbxItemDropTarget = Toolbox.FindParent<ListBoxItem>(hit.VisualHit);
                PageViewModel targetPage;
                int sourceIndex = SelectedDocument.GetPageIndex(draggedPage);
                int targetIndex = SelectedDocument.Pages.Count - 1;

                // Move pageDict to last element if dropped on blank-space
                if (lbxItemDropTarget != null)
                {
                    targetPage = lbxItemDropTarget.DataContext as PageViewModel;
                    targetIndex = SelectedDocument.GetPageIndex(targetPage);
                }
                SelectedDocument.Pages.Move(sourceIndex, targetIndex);

                return;
            }

            // Get any files dropped onto pageview
            AddDroppedPages(FileIO.ExtractDocument(e.Data));
        }
        #endregion
        #endregion
        #endregion

        #region Debugging methods
        private DocumentViewModel AddNewDoc(Models.Document doc)
        {
            int nextIndex = (SelectedDocument != null ? Documents.IndexOf(SelectedDocument) : -1) + 1;

            DocumentViewModel docVM = new DocumentViewModel(doc);
            Documents.Insert(nextIndex, docVM);
            //SelectedDocument = docVM;

            return docVM;
        }

        private PageViewModel AddNewPage(Models.Page page)
        {
            int nextIndex = (SelectedPage != null ? Pages.IndexOf(SelectedPage) : -1) + 1;

            PageViewModel pageVM = new PageViewModel(page);
            SelectedDocument.Pages.Insert(nextIndex, pageVM);
            SelectedPage = pageVM;

            return pageVM;
        }
        #endregion

        #region Page Modifications
        private void RotatePage(PageViewModel page, float rotate)
        {
            if(page!=null)
                page.Rotation += rotate;
        }
        #endregion

        #region Adding new pages
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

            if (SelectedDocument == null)
            {
                SelectedDocument = new DocumentViewModel(new Models.Document());
            }

            // If any files dropped, load their pages
            if (dropFiles != null && dropFiles.Length > 0)
            {
                for (int i = 0; i < dropFiles.Length; i++)
                {
                    AddPages(SelectedDocument, dropFiles[i].pages.ToArray());
                }
            }
        }

        #endregion
        #region Adding new Documents
        [Obsolete("Use 'MainViewModel.AddDocuments' instead")]
        public void CacheDocuments(Models.Document[] documents)
        {
            AddDocuments(documents);
        }

        public void AddDocuments(Models.Document[] documents)
        {
            if (documents == null) throw new ArgumentNullException("Models.Document[] documents");
            if (documents.Length == 0) throw new ArgumentException("Array length is zero. Array needs at least one element.", "Models.Document[] documents");

            foreach (Models.Document doc in documents)
            {
                if (doc == null) continue;

                AddDocument(doc);
            }
        }

        public void AddDocument(Models.Document document)
        {
            if (document == null) throw new ArgumentNullException("Models.Document documents");

            DocumentViewModel docVM;

            docVM = new DocumentViewModel(document);
            Documents.Add(docVM);
            SelectedDocument = docVM;
        }
        #endregion

        #region Rearranging docs/pages
        

        public void MovePage(int oldIndex, int newIndex)
        {
            SelectedDocument.Pages.Move(oldIndex, newIndex);
        }

        public void MoveDocument(int oldIndex, int newIndex)
        {
            Documents.Move(oldIndex, newIndex);
        }

        public void MovePageToDoc(ViewModels.PageViewModel page, ViewModels.DocumentViewModel oldDoc, ViewModels.DocumentViewModel newDoc)
        {
            if (!oldDoc.Pages.Contains(page))
                throw new KeyNotFoundException(page + " not contained within " + oldDoc.Pages);
            if (null == oldDoc)
                throw new NullReferenceException("oldDoc");
            if (null == newDoc)
                throw new NullReferenceException("newDoc");

            oldDoc.Pages.Remove(page);
            newDoc.Pages.Add(page);
        }

        public void SplitDocument(DocumentViewModel docVM, int splitInterval)
        {
            DocumentViewModel newDocVM;
            Models.Document newDoc;
            int docCount = 0;

            if (docVM == null || splitInterval <= 0) return;

            newDocVM = docVM;

            while (docVM.PageCount> splitInterval)
            {
                // current doc VM reached its goal page-count - start the next one...
                if (newDocVM.PageCount >= splitInterval)
                {
                    newDoc = new Models.Document();

                    
                    newDoc.Rename(docVM.DocName + ("." + (++docCount) + "-" + (newDoc.id)));

                    newDoc.image = docVM.Pages[0].Image;

                    newDocVM = new DocumentViewModel(newDoc);
                    //RenameDoc(newDocVM, docVM.DocName, true);
                    Documents.Add(newDocVM);
                    //docCount++;
                }
                else
                {
                    newDocVM.Pages.Add(docVM.Pages[splitInterval]);
                    docVM.Pages.RemoveAt(splitInterval);
                }
            }
        }
        #endregion
        
        #region Utils
        public bool IsValidDropItem(IDataObject data)
        {
            string[] files = data.GetData(DataFormats.FileDrop) as string[];
            return (
                (
                    data.GetDataPresent(typeof(DocumentViewModel)) ||
                    data.GetDataPresent(typeof(PageViewModel)) ||
                    IO.FileIO.IsExtensionSupported(files)
                ));
        }

        private void RenameDoc(DocumentViewModel docVM, string newDocName, bool isNewSubDoc=false)
        {
            if (docVM == null)
                throw new ArgumentNullException("docVM");
            if (String.IsNullOrEmpty(newDocName))
                throw new ArgumentNullException("newDocName");

            if (isNewSubDoc)
            {
                //int subDocID = 1;
                bool isSubIDNew = false;
                string docName;
                string path;
                string ext;

                for (int subDocID = 1; !isSubIDNew; subDocID++)
                {
                    for (int i = 0; i < Documents.Count; i++)
                    {
                        docName = Path.GetFileNameWithoutExtension(Documents[i].DocName);
                        if (!docName.EndsWith("-" + subDocID.ToString()))
                        {
                            path = Path.GetDirectoryName(newDocName) + "\\";
                            ext = Path.GetExtension(newDocName);

                            newDocName = path + docName + "-" + subDocID.ToString() + ext;
                            isSubIDNew = true;
                            //newDocName = "-" + subDocID.ToString();
                            break;
                        }
                    }
                }
            }

            docVM.DocName = newDocName;
        }

        protected void SaveDocument(DocumentViewModel doc)
        {
            IO.FileIO.SaveDocument(doc);
        }
        
        #endregion
    }
}
