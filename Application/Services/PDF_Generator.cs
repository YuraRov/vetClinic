using Core.Interfaces.Services;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;

namespace Application.Services
{
    public class PDF_Generator<K,S>:IPDF_Generator<K,S>
    {
        private readonly ICreateTable<K> _createTable;
        readonly IGetList<K, S> _getlist;
        readonly S _parameters;
        private readonly IConfiguration _configuration;
        private readonly string _fileName;
        private readonly string _contentType;

        public PDF_Generator(
            ICreateTable<K> createTable,
            IGetList<K, S> getlist,
            S parameters,
            IConfiguration configuration)
        {
            _createTable = createTable;
            _getlist = getlist;
            _parameters = parameters;
            _configuration = configuration;
            _fileName = _configuration["Pdf:DefaultFileName"];
            _contentType = _configuration["Pdf:ContentType"];
        }

        public async Task<PdfFileModel> GetPdfFile()
        {
            //Create a new PDF document
            PdfDocument doc = new PdfDocument();
            //Add a page
            PdfPage page = doc.Pages.Add();
            //Create a PdfGrid
            PdfGrid pdfGrid = new PdfGrid();
            //Create list for table
            var list = await _getlist.GetListForPDF(_parameters);
            //Assign data source
            pdfGrid.DataSource = _createTable.CreateMyTable(list);
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
                ContentType = _contentType,
                DefaultFileName = _fileName,
                FileStream = stream
            };

            return pdfFileParam;
        }

    }
}
