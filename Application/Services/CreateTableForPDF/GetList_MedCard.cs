using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Paginator;
using Core.Paginator.Parameters;

namespace Application.Services.CreateTableForPDF
{
    public class GetList_MedCard : IGetList<Appointment, AnimalParameters>
    {
        readonly IAnimalRepository _repository;
        public GetList_MedCard(IAnimalRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<Appointment>> GetListForPDF(AnimalParameters parameters)
        {
            var appointments = await _repository.GetAllAppointmentsWithAnimalIdAsync(parameters);
            return appointments;
        }
    }
}
