using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using PDFToolbox.IO;

namespace PDFToolbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private FileIOLoader _ioLoader;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _ioLoader = new PDFToolboxIOLoader();
            _ioLoader.Load();
        }
    }
}
