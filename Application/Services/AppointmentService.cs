﻿using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ILoggerManager _logger;

        public AppointmentService(
            IAppointmentRepository appointmentRepository, 
            ILoggerManager logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;  
        }

        public async Task CreateAsync(Appointment appointment)
        {
            await _appointmentRepository.InsertAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();
            _logger.LogInfo("Appointment was created in method CreateAsync");
        }

        public async Task DeleteAsync(int appointmentId)
        {
            Appointment? appointment = await GetAsync(appointmentId);

             _appointmentRepository.Delete(appointment);
            await _appointmentRepository.SaveChangesAsync();
            _logger.LogInfo($"Appointment was deleted with Id {appointmentId} in method DeleteAsync");
        }

        public async Task<IEnumerable<Appointment>> GetAsync()
        {
            var appointments = await _appointmentRepository.GetAsync(asNoTracking: true, includeProperties: "AppointmentProcedures.Procedure,AppointmentUsers.User,Animal");
            _logger.LogInfo("Appointments were getted in method GetAsync");
            return appointments;
        }

        public async Task<Appointment> GetAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetById(appointmentId, "AppointmentProcedures.Procedure,AppointmentUsers.User,Animal");
           
            if (appointment is null)
            {
                _logger.LogWarn($"Appointment with id {appointmentId} does not exist");
                throw new NotFoundException($"Appointment with Id {appointmentId} does not exist");
            }

            _logger.LogInfo("Appointment was getted by appointmentId in method GetAsync");
            return appointment;
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            var existingAppointment = await GetAsync(appointment.Id);

            existingAppointment.Date = appointment.Date;
            existingAppointment.MeetHasOccureding = appointment.MeetHasOccureding;
            existingAppointment.Disease = appointment.Disease;
            existingAppointment.AnimalId = appointment.AnimalId;
            existingAppointment.AppointmentUsers = appointment.AppointmentUsers;
            existingAppointment.AppointmentProcedures = appointment.AppointmentProcedures;

            await _appointmentRepository.SaveChangesAsync();
            _logger.LogInfo("Appointment was getted by appointmentId in method UpdateAsync");
        }
    }
}
