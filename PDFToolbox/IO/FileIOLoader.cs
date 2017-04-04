using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFToolbox.IO
{
    abstract class FileIOLoader
    {
        public abstract void Load();

        protected void LoadIO<T>() where T : BaseIOStrategy, new()
        {
            FileIO.RegisterStrategy(new T());
        }
    }
}
