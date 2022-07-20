using Core.Interfaces.Services;
using Core.Paginator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CreateTableForPDF
{
    public class GetList<K,S>: IGetList<K,S>
    {
        public virtual Task<PagedList<K>> GetListForPDF(S parameters)
        {
            var list = new List<K>();
            var res = new PagedList<K>(list, list.Count, 1,1);
            return Task.FromResult(res);
        }
    }
}
