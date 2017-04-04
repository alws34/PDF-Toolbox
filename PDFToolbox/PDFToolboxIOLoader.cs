using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PDFToolbox.IO;

namespace PDFToolbox
{
    class PDFToolboxIOLoader: FileIOLoader
    {
        public override void Load()
        {
            // Load Extractors (pre-loaders)
            LoadIO<OutlookAttachmentExtractor>();
            LoadIO<FileDropExtractor>();

            // Load normal 
            LoadIO<PdfFileIO>();
        }
    }
}
