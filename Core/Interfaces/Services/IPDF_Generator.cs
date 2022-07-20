using Core.Models;

namespace Core.Interfaces.Services
{
    public interface IPDF_Generator<K,S>
    {
        Task<PdfFileModel> GetPdfFile();
    }
}
