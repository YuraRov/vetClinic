﻿using Core.Entities;
using Core.ViewModel;
using Core.ViewModels.AnimalViewModel;
using Core.ViewModels.ProcedureViewModels;
using Microsoft.Extensions.DependencyInjection;
using WebApi.AutoMapper.AnimalMappers;
using WebApi.AutoMapper.Interface;
using WebApi.AutoMapper.ProcedureMappers;

namespace WebApi.Configurations;

public static class ApplicationMappersConfiguration
{
    public static void AddApplicationMappers(this IServiceCollection services)
    {
        services.AddScoped<IViewModelMapper<ProcedureViewModelBase, Procedure>, ProcedureMapper>();
        services.AddScoped<IViewModelMapperAsync<IEnumerable<ProcedureSpecialization>, IEnumerable<SpecializationViewModel>>,
            SpecializationProcedureToSpecViewModel>();
        services.AddScoped<IViewModelMapperAsync<Procedure, ProcedureSpecViewModel>, ProcedureSpecViewModelMapper>();
        //animal
        services.AddScoped<IViewModelMapper<AnimalViewModel, Animal>, AnimalMapper>();
        services.AddScoped<IViewModelMapperAsync<Animal, AnimalViewModel>, AnimalMapperAsync>();
    }
}