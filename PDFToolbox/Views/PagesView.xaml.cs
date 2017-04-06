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

namespace PDFToolbox.Views
{
    /// <summary>
    /// Interaction logic for PagesView.xaml
    /// </summary>
    public partial class PagesView : UserControl
    {
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

        public PagesView()
        {
            InitializeComponent();
        }


        private void lbxPages_Drop(object sender, DragEventArgs e)
        {
            //TODO: Need to copy from MainWindow.xaml.cs.  Or do it right this time.
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



        private static DependencyProperty RegisterProperty<T>(string varName, T defaultValue = default(T))
        {
            return DependencyProperty.Register(varName, typeof(T), typeof(PagesView), new PropertyMetadata(defaultValue));
        }
    }
}
