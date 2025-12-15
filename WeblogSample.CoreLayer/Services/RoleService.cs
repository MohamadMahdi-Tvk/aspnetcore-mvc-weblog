using Microsoft.EntityFrameworkCore;
using WeblogSample.Data.Contexts;
using WeblogSample.Service.DTOs.Roles;
using WeblogSample.Service.Interfaces;
using WeblogSample.Service.Mappers.Roles;

namespace WeblogSample.Service.Services;

public class RoleService : IRoleService
{
    private readonly DataBaseContext _db;
    private readonly RoleMapper _mapper;

    public RoleService(DataBaseContext db, RoleMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<List<RoleDto>> GetAllAsync()
    {
        var list = await _db.Roles
            .AsNoTracking()
            .ToListAsync();

        return list.Select(r => _mapper.ToDto(r)).ToList();
    }

    public async Task<RoleDto> GetByIdAsync(short id)
    {
        var entity = await _db.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

        return entity == null ? null : _mapper.ToDto(entity);
    }

    public async Task<short> CreateAsync(RoleDto dto)
    {
        var entity = _mapper.ToEntity(dto);

        _db.Roles.Add(entity);
        await _db.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(RoleDto dto)
    {
        var entity = await _db.Roles.FindAsync(dto.Id);
        if (entity == null) return false;

        _mapper.UpdateEntity(dto, entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(short id)
    {
        var entity = await _db.Roles.FindAsync(id);
        if (entity == null) return false;

        _db.Roles.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

}
