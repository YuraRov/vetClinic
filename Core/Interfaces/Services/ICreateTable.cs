using Core.Paginator;
using System.Data;

namespace Core.Interfaces.Services
{
    public interface ICreateTable<T>
    {
        DataTable CreateMyTable(PagedList<T> list);
    }
}
