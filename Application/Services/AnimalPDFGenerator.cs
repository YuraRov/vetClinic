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
    public class AnimalPDFGenerator : IPdfGenerator<AnimalParameters>
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalPDFGenerator(IAnimalRepository animalRepository, IConfiguration configuration)
        {
            _animalRepository = animalRepository;
        }

        public async Task<DataTable> CreateTabel(AnimalParameters animalParameters)
        {
            var appointments = await _animalRepository.GetAllAppointmentsWithAnimalIdAsync(animalParameters);

            DataTable dataTable = new DataTable();
            //Add columns to the DataTable
            dataTable.Columns.Add("Data");
            dataTable.Columns.Add("Disease");
            //Add rows to the DataTable
            foreach (var x in appointments)
            {
                dataTable.Rows.Add(new object[] { $"{x.Date}", $"{x.Disease}" });
            }

            return dataTable;
        }
    }
}
