﻿using Core.Entities;
using Core.Interfaces.Repositories;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ProcedureRepository : IProcedureRepository
{
    private readonly ClinicContext _clinicContext;
    private readonly ISpecializationRepository _specializationRepository;

    public ProcedureRepository(
        ClinicContext clinicContext, 
        ISpecializationRepository specializationRepository)
    {
        _clinicContext = clinicContext;
        _specializationRepository = specializationRepository;
    }

    public async Task<IEnumerable<Procedure>> GetAllProceduresAsync()
    { 
        return await _clinicContext.Procedures
            .Include(procedure => procedure.ProcedureSpecializations)
            .ThenInclude(ps => ps.Specialization)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Procedure?> GetProcedureByIdAsync(int procedureId)
    {
        return await _clinicContext.Procedures
            .Include(procedure => procedure.ProcedureSpecializations)
            .AsNoTracking()
            .SingleOrDefaultAsync(pr => pr.Id == procedureId);
    }

    public async Task<Procedure> AddProcedureAsync(Procedure procedure)
    {
        var result = await _clinicContext.Procedures.AddAsync(procedure);
        return result.Entity;
    }

    public async Task UpdateProcedureSpecializationsAsync(Procedure procedure, IEnumerable<int> specializationIds)
    {
        _clinicContext.ProcedureSpecializations.RemoveRange(procedure.ProcedureSpecializations);
        await SaveChangesAsync();
        
        foreach (var specializationId in specializationIds)
        {
            await _specializationRepository.GetSpecializationByIdAsync(specializationId);
            await _clinicContext.ProcedureSpecializations.AddAsync(new ProcedureSpecialization()
            {
                ProcedureId = procedure.Id,
                SpecializationId = specializationId
            });
        }
    }

    public async Task DeleteProcedureAsync(Procedure procedure)
    {
        _clinicContext.Procedures.Remove(procedure);
    }

    public async Task UpdateProcedureAsync(Procedure procedure)
    {
        _clinicContext.Procedures.Update(procedure);
    }

    public async Task SaveChangesAsync()
    {
        await _clinicContext.SaveChangesAsync();
    }
}