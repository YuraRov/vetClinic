using Core.Models;
using Core.Paginator.Parameters;
using Microsoft.Extensions.Configuration;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IPdfGenerator<T> where T : class
    {
        public PdfFileModel GetPdfFile(T paramert)
        {
            PdfDocument doc = new PdfDocument();
            //Add a page
            PdfPage page = doc.Pages.Add();
            //Create a PdfGrid
            PdfGrid pdfGrid = new PdfGrid();
            //Create a DataTable
            pdfGrid.DataSource = CreateTabel(paramert).Result;
            //Draw grid to the page of PDF document
            pdfGrid.Draw(page, new PointF(10, 10));
            //Save the PDF document to stream
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;
            //Close the document.
            doc.Close(true);
            //Creates a FileContentResult object by using the file contents, content type, and file name.

            var pdfFileParam = new PdfFileModel
            {
                ContentType = "application/pdf",
                DefaultFileName = "DefaultName",
                FileStream = stream
            };

            return pdfFileParam;
        }

        public Task<DataTable> CreateTabel(T paramert);
    }
}
