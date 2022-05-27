﻿using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.ViewModels.ProcedureViewModels;
using Core.ViewModels.SpecializationViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApi.AutoMapper.Interface;

namespace WebApi.Controllers
{
    [Route("api/specialization")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        readonly ISpecializationService _service;
        readonly IViewModelMapper<SpecializationViewModel, Specialization> _mapper;
        readonly IViewModelMapper<Specialization, SpecializationViewModel> _viewModelMapper;
        readonly IViewModelMapper<IEnumerable<Specialization>, IEnumerable<SpecializationViewModel>> _listMapper;
        IEnumerableViewModelMapper<IEnumerable<Procedure>, IEnumerable<ProcedureReadViewModel>>
     _procedureEnumerableViewModelMapper;
        public SpecializationController(
            ISpecializationService service, 
            IViewModelMapper<SpecializationViewModel, Specialization> mapper, 
            IViewModelMapper<Specialization, SpecializationViewModel> viewModelMapper, 
            IViewModelMapper<IEnumerable<Specialization>, IEnumerable<SpecializationViewModel>> listMapper,
            IEnumerableViewModelMapper<IEnumerable<Procedure>, IEnumerable<ProcedureReadViewModel>> procedureEnumerableViewModelMapper)
        {
            _service = service;
            _mapper = mapper;
            _viewModelMapper = viewModelMapper;
            _listMapper = listMapper;
            _procedureEnumerableViewModelMapper = procedureEnumerableViewModelMapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetSpecializations()
        {
            return Ok(_listMapper.Map(await _service.GetAllSpecializationsAsync()));
        }

        [HttpGet("/{id:int:min(1)}")]
        public async Task<ActionResult> GetSpecializationById([FromRoute] int id)
        {
            return Ok(_viewModelMapper.Map(await _service.GetSpecializationByIdAsync(id)));
        }

        [HttpGet("{id:int:min(1)}/procedures")]
        public async Task<ActionResult> GetSpecializationProcedures([FromRoute] int id)
        {
            return Ok(_procedureEnumerableViewModelMapper.Map(await _service.GetSpecializationProcedures(id)));
        }

        [HttpPost]
        public async Task<ActionResult> AddSpecialization([FromBody]SpecializationViewModel specialization)
        {
               return Ok(_viewModelMapper.Map(
                    await _service.AddSpecializationAsync(_mapper.Map(specialization))));
        }

        [HttpPut("/{id:int:min(1)}")]
        public async Task<ActionResult> UpdateSpecialization([FromRoute]int id, [FromBody]SpecializationViewModel updated)
        {
            await _service.UpdateSpecializationAsync(id,_mapper.Map(updated));
            return NoContent();
        }

        [HttpDelete("/{id:int:min(1)}")]
        public async Task<ActionResult> DeleteSpecialization([FromRoute] int id)
        {
            await _service.DeleteSpecializationAsync(id);
            return NoContent();
        }
    }
}
