using Core.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Interfaces.Repositories
{
    public interface ISpecializationRepository : IRepository<Specialization>
    {
        Task UpdateProceduresAsync(int specializationId, IEnumerable<int> procedureIds);
        Task UpdateUsersAsync(int specializationId, IEnumerable<int> userIds);
    }
}
