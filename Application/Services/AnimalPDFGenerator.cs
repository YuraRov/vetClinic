using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AnimalPDFGenerator
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalPDFGenerator(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }


    }
}
