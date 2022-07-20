using Core.Interfaces.Services;
using Core.Paginator;
using System.Data;

namespace Application.Services.CreateTableForPDF
{
    public class CreateTable<T>: ICreateTable<T>
    {
        public virtual DataTable CreateMyTable(PagedList<T> list)
        {
            var table = new DataTable();
            return table;
        }
    }
}
