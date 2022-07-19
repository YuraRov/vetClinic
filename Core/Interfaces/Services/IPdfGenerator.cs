using Core.Models;
using Core.Paginator.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IPdfGenerator
    {
        public Task<PdfFileModel> GetPdfFile(AnimalParameters animalParameters);
    }
}
