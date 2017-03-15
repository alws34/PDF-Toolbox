using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;

using iTextSharp.text.pdf;

namespace PDFToolbox.Models
{
    public class Page
    {
        // image used to display in pageDict/document views
        public BitmapImage image { get; set; }
        // Original document file name
        public string fName { get; set; }
        // Original pageDict number
        public int number { get; set; }
        // Rotation of pageDict (in degrees)
        //public PdfNumber rotation { get; set; }
        public float rotation { get; set; }
        // Original rotation of pageDict (in degrees)
        //public PdfNumber originalRotation { get; set; }
        public float originalRotation { get; set; }
        // True = image is ignored on PDF creation; False = image is used
        public bool isImagePreview { get; set; }

        public Stream imageStream { get; set; }

        public ObservableCollection<Common.UIString> uiStrings { get; set; }

        private static int nextID = 0;
        public int id { get; private set; }

        public Page()
        {
            id = nextID++;
            rotation = 0f; //new PdfNumber(0f);
            originalRotation = 0f; //new PdfNumber(0f);
        }

        public void Copy(Page page)
        {
            if (page == null) return;

            image = page.image;
            fName = page.fName;
            number = page.number;
            rotation = page.rotation;
            originalRotation = page.originalRotation;
            isImagePreview = page.isImagePreview;
            imageStream = page.imageStream;
            uiStrings = page.uiStrings;
        }

        public static Page MakeCopy(Page page)
        {
            if (page == null) throw new ArgumentNullException("Page page");

            Page p = new Page();

            p.Copy(page);

            return p;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            Page p;
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            p = (Page)obj;
            if (fName == p.fName &&
               image == p.image &&
               isImagePreview == p.isImagePreview &&
               number == p.number &&
               originalRotation == p.originalRotation &&
               rotation == p.rotation)
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

        [Obsolete]
        public void SetRotation(float rotation)
        {
            this.rotation = rotation; // new PdfNumber(rotation);
        }
    }
}
