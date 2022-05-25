﻿using Core.Entities;
using Core.Interfaces.Services;
using Core.ViewModels.AnimalViewModel;
using Microsoft.AspNetCore.Mvc;
using WebApi.AutoMapper.Interface;
using WebApi.Validators;
using Core.Exceptions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        private readonly IViewModelMapper<AnimalViewModel, Animal> _mapperVMtoM;
        private readonly IViewModelMapper<Animal, AnimalViewModel> _mapperMtoVM;
        private readonly AnimalViewModelValidator _validator;

        public AnimalController(IAnimalService animalService, IViewModelMapper<AnimalViewModel, Animal> mapperVMtoM, IViewModelMapper<Animal, AnimalViewModel> mapperMtoVM, AnimalViewModelValidator validator)
        {
            _animalService = animalService;
            _mapperVMtoM = mapperVMtoM;
            _mapperMtoVM = mapperMtoVM;
            _validator = validator;
        }

        [HttpGet("/api/[controller]/")]
        public async Task<ActionResult<IEnumerable<AnimalViewModel>>> GetAsync()
        {
            var animals = await _animalService.GetAllAnimalsAsync();
            var map = new List<AnimalViewModel>();
            foreach (var animal in animals)
            {
                map.Add(_mapperMtoVM.Map(animal));
            }
            return Ok(map);
        }

        [HttpGet("/api/[controller]/{id:int:min(1)}")]
        public async Task<ActionResult<AnimalViewModel>> GetAsync(int id)
        {
            var animal = await _animalService.GetAnimalByIdAsync(id);
            var map = _mapperMtoVM.Map(animal);
            return Ok(map);
        }

        [HttpPost("/api/[controller]/")]
        public async Task<ActionResult> PostAsync(AnimalViewModel model)
        {
            var validResult = await _validator.ValidateAsync(model);
            if (!validResult.IsValid)
            {
                throw new BadRequestException(validResult.Errors.ToString());
            }

            await _animalService.AddNewAnimalAsync(_mapperVMtoM.Map(model));
            return Ok();
        }

        [HttpDelete("/api/[controller]/")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _animalService.DeleteAnimalAsync(id);
            return NoContent();
        }

        [HttpPut("/api/[controller]/")]
        public async Task<ActionResult> UpdateAsync(AnimalViewModel model)
        {
            var validResult = await _validator.ValidateAsync(model);
            if (!validResult.IsValid)
            {
                throw new BadRequestException(validResult.Errors.ToString());
            }

            var map = _mapperVMtoM.Map(model);
            await _animalService.UpdateAnimalAsync(map);
            return Ok();
        }
        

    }
}
