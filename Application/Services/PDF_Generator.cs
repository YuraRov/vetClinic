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
    public class PDF_Generator<T,K>
    {
        private readonly T _repository;
        private readonly IConfiguration _configuration;
        private readonly string _fileName;
        private readonly string _contentType;

        public PDF_Generator(T repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _fileName = _configuration["Pdf:DefaultFileName"];
            _contentType = _configuration["Pdf:ContentType"];
        }

        public async Task<PdfFileModel> GetPdfFile(K parameters)

    }
}
