using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Paginator.Parameters;
using Microsoft.AspNetCore.Mvc;
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

namespace Application.Services
{
    public class AnimalPDFGenerator : IPdfGenerator
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IConfiguration _configuration;
        private readonly string _fileName;
        private readonly string _contentType;

        public AnimalPDFGenerator(IAnimalRepository animalRepository, IConfiguration configuration)
        {
            _animalRepository = animalRepository;
            _configuration = configuration;
            _fileName = _configuration["Pdf:DefaultFileName"];
            _contentType = _configuration["Pdf:ContentType"];
        }

        public async Task<PdfFileModel> GetPdfFile(AnimalParameters animalParameters)
        {
            var appointments = await _animalRepository.GetAllAppointmentsWithAnimalIdAsync(animalParameters);
            //Create a new PDF document
            PdfDocument doc = new PdfDocument();
            //Add a page
            PdfPage page = doc.Pages.Add();
            //Create a PdfGrid
            PdfGrid pdfGrid = new PdfGrid();
            //Create a DataTable
            DataTable dataTable = new DataTable();
            //Add columns to the DataTable
            dataTable.Columns.Add("Data");
            dataTable.Columns.Add("Disease");
            //Add rows to the DataTable
            foreach (var x in appointments)
            {
                dataTable.Rows.Add(new object[] { $"{x.Date}", $"{x.Disease}" });
            }
            //Assign data source
            pdfGrid.DataSource = dataTable;
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
