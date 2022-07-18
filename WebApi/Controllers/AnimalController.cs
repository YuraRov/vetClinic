using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModels.AnimalViewModel;
using Microsoft.AspNetCore.Mvc;
using WebApi.AutoMapper.Interface;
using Core.ViewModels;
using Core.Paginator;
using Core.Paginator.Parameters;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System.Data;
using Syncfusion.Drawing;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        private readonly IViewModelMapperUpdater<AnimalViewModel, Animal> _mapperVMtoM;
        private readonly IEnumerableViewModelMapper<IEnumerable<Animal>, IEnumerable<AnimalViewModel>> _mapperAnimalListToList;
        private readonly IViewModelMapper<PagedList<Appointment>, PagedReadViewModel<AnimalMedCardViewModel>> _pagedMedCardMapper;

        public AnimalController(
            IAnimalService animalService,
            IViewModelMapperUpdater<AnimalViewModel, Animal> mapperVMtoM,
            IEnumerableViewModelMapper<IEnumerable<Animal>, IEnumerable<AnimalViewModel>> mapperAnimalListToList,
            IViewModelMapper<PagedList<Appointment>, PagedReadViewModel<AnimalMedCardViewModel>> pagedMedCardMapper)
        {
            _animalService = animalService;
            _mapperVMtoM = mapperVMtoM;
            _mapperAnimalListToList = mapperAnimalListToList;
            _pagedMedCardMapper = pagedMedCardMapper;
        }

        [HttpGet("{ownerId:int:min(1)}")]
        public async Task<IEnumerable<AnimalViewModel>> GetAsync([FromRoute] int ownerId)
        {
            var animals = await _animalService.GetAsync(ownerId);
            var map = _mapperAnimalListToList.Map(animals);
            return map;
        }

        [HttpGet("medcard")]
        public async Task<PagedReadViewModel<AnimalMedCardViewModel>> GetMedCardAsync([FromQuery] AnimalParameters animalParameters)
        {
            var appointments = await _animalService.GetAllAppointmentsWithAnimalIdAsync(animalParameters);
            var map = _pagedMedCardMapper.Map(appointments);
            return map;
        }

        [HttpGet("generatePDF")]
        public async Task<FileStreamResult> GeneratePDF([FromQuery] AnimalParameters animalParameters)
        {
            var appointments = await _animalService.GetAllAppointmentsWithAnimalIdAsync(animalParameters);

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
            foreach(var x in appointments)
            {
                dataTable.Rows.Add(new object[] { $"{x.Date}", $"{x.Disease}"});
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
            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = "Output.pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);
            //FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
            //fileStreamResult.FileDownloadName = "Sample.pdf";
            //return fileStreamResult;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody]AnimalViewModel model)
        {
            var map = _mapperVMtoM.Map(model);
            await _animalService.CreateAsync(map);
            return Ok();
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> DeleteAsync([FromRoute]int id)
        {
            await _animalService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody]AnimalViewModel model)
        {
            var prevAnimal = await _animalService.GetByIdAsync(model.Id);
            _mapperVMtoM.Map(model, prevAnimal);
            await _animalService.UpdateAsync(prevAnimal);
            return NoContent();
        }
    }
}
