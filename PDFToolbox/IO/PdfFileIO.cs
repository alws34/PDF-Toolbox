using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows;

using iTextSharp.text.pdf;

namespace PDFToolbox.IO
{
    public class PdfFileIO : BaseFileIOStrategy
    {
        public PdfFileIO()
            : base()
        {
            SetSupportedExtensions("PDF");
        }

        public override Models.Document LoadDocument(FileIOInfo info)
        {
            //Bitmap img;
            //MemoryStream memStream;
            string tmpFile;
            Models.Page page;
            Models.Document doc;
            List<BitmapImage> pageImages = new List<BitmapImage>();
            PdfReader reader;
            //List<Models.Page> pages;

            //FIXME: handle temp file paths moar better...
            tmpFile = (info.IsTempPath ? info.FullFileName : CopyToTemp(info.FullFileName));
            if (string.IsNullOrEmpty(tmpFile)) return null;

            /*
            memStream = Load(tmpFile);
            // Stream is null if fPath is invalid or unsupported
            if (memStream == null) return null;
            */

            doc = new Models.Document();

            pageImages = GetPdfPageImages(tmpFile);
            
            reader = new PdfReader(tmpFile);

            for (int i = 0; i < reader.NumberOfPages; i++)
            {
                page = CachePdfPageFromFile(info, reader, i+1);
                page.image = pageImages[i];
                
                doc.pages.Add(new ViewModels.PageViewModel(page));
            }

            if (doc.pages.Count > 0)
            {
                doc.image = doc.pages[0].Image;
                doc.fName = doc.pages[0].DisplayName;
            }

            return doc;
        }
        public override void SaveDocument(ViewModels.DocumentViewModel document)
        {
            string srcDocPath;
            string targetFilePath = SafeFilePath(document.DocName);
            Stream stream;
            iTextSharp.text.Image image;
            PdfDictionary pageDict;
            PdfImportedPage importedPage;
            PdfContentByte contentByte;
            //iTextSharp.text.Paragraph para;
            PdfCopy targetPdf;
            iTextSharp.text.Document doc;
            //iTextSharp.text.pdf.BaseFont baseFont;
            //iTextSharp.text.Font font;
            PdfReader srcReader;
            float pageRotationInRads;
            System.Windows.Point pageOrigin;
            System.Windows.Point stringOffset;
            //ColumnText ct;
            PdfCopy.PageStamp pageStamp;

            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(targetFilePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath));

                stream = new FileStream(targetFilePath, FileMode.Create);

                doc = new iTextSharp.text.Document();
                targetPdf = new PdfCopy(doc, stream);
                doc.Open();

                //baseFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ROMAN, iTextSharp.text.pdf.BaseFont.CP1252, false);
                //font = new iTextSharp.text.Font(baseFont, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);

                foreach (ViewModels.PageViewModel vm in document.Pages)
                {
                    srcDocPath = FileIO.ToTempFileName(vm.DisplayName);
                    pageRotationInRads = vm.Rotation * (float)(Math.PI / 180);


                    // Copy pageDict from source...
                    if (Path.GetExtension(srcDocPath).ToUpperInvariant() == ".PDF")
                    {
                        srcReader = new iTextSharp.text.pdf.PdfReader(srcDocPath);
                        pageDict = srcReader.GetPageN(vm.PageNumber - 1);
                        importedPage = targetPdf.GetImportedPage(srcReader, vm.PageNumber - 1);
                        pageStamp = targetPdf.CreatePageStamp(importedPage);
                        //pageOrigin = new System.Windows.Point(Math.Cos(pageRotationInRads - (90f * (float)(Math.PI / 180))) * importedPage.Width,
                        //                                      Math.Sin(pageRotationInRads - (180f * (float)(Math.PI / 180))) * importedPage.Height);
                        pageOrigin = new System.Windows.Point(Math.Cos(pageRotationInRads) * importedPage.Width,
                                                               Math.Sin(pageRotationInRads) * importedPage.Height);

                        //add any strings
                        foreach (Common.UIString str in vm.Strings)
                        {
                            // account for page rotation
                            stringOffset = new System.Windows.Point(Math.Cos(pageRotationInRads) * str.X,
                                                                    Math.Sin(pageRotationInRads) * str.Y);
                            
                            ColumnText.ShowTextAligned(pageStamp.GetOverContent(),
                                iTextSharp.text.Element.ALIGN_LEFT,
                                new iTextSharp.text.Phrase(str.String),
                                (float)stringOffset.X,
                                (float)stringOffset.Y,
                                0);
                        }
                        // apply any added rotation
                        pageDict.Put(PdfName.ROTATE, new PdfNumber((vm.Rotation) % 360f));
                        pageStamp.AlterContents();
                        targetPdf.AddPage(importedPage);
                        
                        targetPdf.FreeReader(srcReader);
                        srcReader.Close();
                    }

                    if (vm.ImageStream != null && targetPdf.NewPage())
                    {
                        contentByte = new PdfContentByte(targetPdf);

                        image = iTextSharp.text.Image.GetInstance(vm.ImageStream);

                        image.ScalePercent(72f / image.DpiX * 100);
                        image.SetAbsolutePosition(0, 0);

                        contentByte.AddImage(image);
                        contentByte.ToPdf(targetPdf);

                    }
                }
                targetPdf.Close();
                doc.Close();
                stream.Close();
            }
            catch (System.IO.IOException e)
            {
                Toolbox.MessageBox(e.Message);
            }
            catch (Exception e)
            {
                Toolbox.MessageBoxException(e);
            }
            finally
            {
                //if (targetPdf != null)
                //    targetPdf.Close();

                //doc.Close();
                //memStream.Close();
            }
        }

        private Models.Page CachePdfPageFromFile(FileIOInfo info, PdfReader reader, int pageNum)
        {
            Models.Page page = new Models.Page();
            PdfNumber num = new PdfNumber(reader.GetPageRotation(pageNum));
            page.number = ++pageNum;
            page.fName = (info.IsTempPath ? info.FileName : info.FullFileName);
            //page.originalRotation = new PdfNumber(reader.GetPageRotation(pageNum));
            page.originalRotation = num.FloatValue;

            return page;
        }

        private BitmapImage GetPdfPageImage(PdfiumViewer.PdfDocument pDoc, int pageNum)
        {
            BitmapImage image = new BitmapImage();
            Bitmap img;
            MemoryStream stream = new MemoryStream();

            img = (Bitmap)pDoc.Render(pageNum, 96.0f, 96.0f, true);
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);


            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = stream;
            image.EndInit();
            image.Freeze();

            return image;
        }
        private List<BitmapImage> GetPdfPageImages(string fName)
        {
            BitmapImage img;
            List<BitmapImage> images = new List<BitmapImage>();
            PdfiumViewer.PdfDocument pDoc = PdfiumViewer.PdfDocument.Load(fName);

            for (int i = 0; i < pDoc.PageCount; i++)
            {
                img = GetPdfPageImage(pDoc, i);

                if(img!=null)
                {
                    images.Add(img);
                }
                else
                {
                    images.Add(new BitmapImage());
                }
            }

            pDoc.Dispose();
            return images;
        }
    }
}
