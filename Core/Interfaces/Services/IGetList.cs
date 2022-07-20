using Core.Paginator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IGetList<K,S>
    {
        Task<PagedList<K>> GetListForPDF(S parameters);
    }
}
