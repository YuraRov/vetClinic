using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PdfFileModel
    {
        public MemoryStream? FileStream { get; set; }
        public string? DefaultFileName { get; set; }
        public string? ContentType { get; set; }
    }
}
