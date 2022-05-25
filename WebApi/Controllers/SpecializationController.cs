﻿using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.ViewModels.SpecializationViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApi.AutoMapper.Interface;

namespace WebApi.Controllers
{
    [Route("api/specialization")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        ISpecializationService _service;
        IViewModelMapper<SpecializationViewModel, Specialization> _mapper;
        IViewModelMapper<Specialization, SpecializationViewModel> _viewModelMapper;

        public SpecializationController(
            ISpecializationService service, 
            IViewModelMapper<SpecializationViewModel, Specialization> mapper, IViewModelMapper<Specialization, 
            SpecializationViewModel> viewModelMapper)
        {
            _service = service;
            _mapper = mapper;
            _viewModelMapper = viewModelMapper;
        }

        [HttpGet()]
        public async Task<ActionResult> GetSpecializations()
        {
            var res = await _service.GetAllSpecializationsAsync();
            var viewModels = new List<SpecializationViewModel>();
            foreach (var spec in res)
                viewModels.Add(_viewModelMapper.Map(spec));
            return Ok(viewModels);
        }

        [HttpGet("api/specialization/{id:int:min(1)}")]
        public async Task<ActionResult> GetSpecializationById([FromRoute] int id)
        {
            return Ok(_viewModelMapper.Map(await _service.GetSpecializationByIdAsync(id)));
        }

        [HttpPost]
        public async Task<ActionResult> AddSpecialization(SpecializationViewModel specialization)
        {
            return !ModelState.IsValid ? throw new BadRequestException() :
                Ok(_viewModelMapper.Map(
                    await _service.AddSpecializationAsync(_mapper.Map(specialization))));
        }


        [HttpPut("api/specialization/{id:int:min(1)}")]
        public async Task<ActionResult> UpdateSpecialization([FromRoute]int id, SpecializationViewModel updated)
        {
            if(!ModelState.IsValid)
                throw new BadRequestException("invalid parameters");
            await _service.UpdateSpecializationAsync(_mapper.Map(updated));
            return Ok();
        }

        [HttpDelete("api/specialization/{id:int:min(1)}")]
        public async Task<ActionResult> DeleteSpecialization([FromRoute] int id)
        {
            await _service.DeleteSpecializationAsync(id);
            return Ok();
        }
    }
}
