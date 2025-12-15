using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Data.Contexts;
using WeblogSample.Service.DTOs.Categories;
using WeblogSample.Service.Interfaces;
using WeblogSample.Service.Mappers.Categories;

namespace WeblogSample.Service.Services;

public class CategoryService : ICategoryService
{
    private readonly DataBaseContext _db;
    private readonly CategoryMapper _mapper;

    public CategoryService(DataBaseContext db, CategoryMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var list = await _db.Categories
            .AsNoTracking()
            .ToListAsync();

        return list.Select(c => _mapper.ToDto(c)).ToList();
    }

    public async Task<CategoryDto> GetByIdAsync(short id)
    {
        var entity = await _db.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return entity == null ? null : _mapper.ToDto(entity);
    }

    public async Task<short> CreateAsync(CategoryDto dto)
    {
        var entity = _mapper.ToEntity(dto);
        _db.Categories.Add(entity);
        await _db.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(CategoryDto dto)
    {
        var entity = await _db.Categories.FindAsync(dto.Id);
        if (entity == null) return false;

        _mapper.UpdateEntity(dto, entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(short id)
    {
        var entity = await _db.Categories.FindAsync(id);
        if (entity == null) return false;

        _db.Categories.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}
