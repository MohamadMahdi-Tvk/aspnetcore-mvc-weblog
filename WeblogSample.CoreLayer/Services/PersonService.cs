using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Data.Contexts;
using WeblogSample.Service.DTOs.Persons;
using WeblogSample.Service.Interfaces;
using WeblogSample.Service.Mappers.Persons;

namespace WeblogSample.Service.Services;

public class PersonService : IPersonService
{
    private readonly DataBaseContext _db;
    private readonly PersonMapper _mapper;

    public PersonService(DataBaseContext db, PersonMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<List<PersonDto>> GetAllAsync()
    {
        var list = await _db.People
            .AsNoTracking()
            .Include(p => p.Role)
            .ToListAsync();

        return list.Select(p => _mapper.ToDto(p)).ToList();
    }

    public async Task<PersonDto> GetByIdAsync(long id)
    {
        var entity = await _db.People
            .AsNoTracking()
            .Include(p => p.Role)
            .FirstOrDefaultAsync(p => p.Id == id);

        return entity == null ? null : _mapper.ToDto(entity);
    }

    public async Task<long> CreateAsync(PersonCreateDto dto)
    {
        var exist = await _db.People
            .AnyAsync(p => p.UserName == dto.UserName);

        if (exist)
            throw new Exception("Username already exists.");

        var entity = _mapper.ToEntity(dto);

        _db.People.Add(entity);
        await _db.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(PersonUpdateDto dto)
    {
        var entity = await _db.People.FindAsync(dto.Id);
        if (entity == null) return false;

        _mapper.UpdateEntity(dto, entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task ToggleActiveAsync(long id)
    {
        var user = await _db.People.FindAsync(id)
            ?? throw new Exception("User not found");

        user.IsActive = !user.IsActive;
        await _db.SaveChangesAsync();
    }

}
