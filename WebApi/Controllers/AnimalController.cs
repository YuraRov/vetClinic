﻿using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModels.AnimalViewModel;
using Microsoft.AspNetCore.Mvc;
using WebApi.AutoMapper.Interface;
using WebApi.Validators;
using Core.Exceptions;
using Core.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/animal")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        private readonly IViewModelMapper<AnimalViewModel, Animal> _mapperVMtoM;
        private readonly IViewModelMapper<Animal, AnimalViewModel> _mapperMtoVM;
        private readonly IEnumerableViewModelMapper<IEnumerable<Animal>, IEnumerable<AnimalViewModel>> _mapperAnimalListToList;
        private readonly IEnumerableViewModelMapper<IEnumerable<Appointment>, IEnumerable<AppointmentViewModel>> _mapperMedCard;

        public AnimalController(
            IAnimalService animalService,
            IViewModelMapper<AnimalViewModel, Animal> mapperVMtoM,
            IViewModelMapper<Animal, AnimalViewModel> mapperMtoVM,
            IEnumerableViewModelMapper<IEnumerable<Animal>, IEnumerable<AnimalViewModel>> mapperAnimalListToList,
            IEnumerableViewModelMapper<IEnumerable<Appointment>, IEnumerable<AppointmentViewModel>> mapperMedCard)
        {
            _animalService = animalService;
            _mapperVMtoM = mapperVMtoM;
            _mapperMtoVM = mapperMtoVM;
            _mapperAnimalListToList = mapperAnimalListToList;
            _mapperMedCard = mapperMedCard;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalViewModel>>> GetAsync()
        {
            var animals = await _animalService.GetAllAnimalsAsync();
            var map = _mapperAnimalListToList.Map(animals);
            return Ok(map);
        }

        [HttpGet("/medcard/{id:int:min(1)}")]
        public async Task<ActionResult<IEnumerable<AppointmentViewModel>>> GetMedCardAsync([FromRoute] int id)
        {
            var appointments = await _animalService.GetAllAppointmentsWithAnimalIdAsync(id);
            var map = _mapperMedCard.Map(appointments);
            return Ok(map);
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<AnimalViewModel>> GetAsync([FromRoute]int id)
        {
            var animal = await _animalService.GetAnimalByIdAsync(id);
            var map = _mapperMtoVM.Map(animal);
            return Ok(map);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody]AnimalViewModel model)
        {
            var map = _mapperVMtoM.Map(model);
            var newAnimal =  await _animalService.AddNewAnimalAsync(map);
            return Created(nameof(GetAsync),newAnimal);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromRoute]int id)
        {
            await _animalService.DeleteAnimalAsync(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(AnimalViewModel model)
        {

            var map = _mapperVMtoM.Map(model);
            await _animalService.UpdateAnimalAsync(map);
            return NoContent();
        }
        

    }
}
