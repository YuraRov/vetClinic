﻿using Core.Entities;
using Core.Interfaces.Repositories;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ClinicContext _clinicContext;

        public AnimalRepository(ClinicContext clinicContext)
        {
            _clinicContext = clinicContext;
        }

        public async Task AddNewAnimalAsync(Animal animal)
        {
            await _clinicContext.Animals.AddAsync(animal);
        }

        public async Task DeleteAnimalAsync(Animal animal)
        {
            _clinicContext.Animals.Remove(animal);
        }

        public async Task<IEnumerable<Animal>> GetAllAnimalsAsync()
        {
            var result = await _clinicContext.Animals.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsWithAnimalIdAsync(int id)
        {
            var result = await _clinicContext.Appointments.Where(appointment => appointment.Id == id).ToListAsync();
            return result;
        }

        public async Task<Animal?> GetAnimalByIdAsync(int animalId)
        {
            var animal = await _clinicContext.Animals.SingleOrDefaultAsync(animal => animal.Id == animalId);
            return animal;
        }

        public async Task UpdateAnimalAsync(Animal animal)
        {
            _clinicContext.Animals.Update(animal);
        }

        public async Task SaveChangesAsync()
        {
            await _clinicContext.SaveChangesAsync();
        }
    }
}