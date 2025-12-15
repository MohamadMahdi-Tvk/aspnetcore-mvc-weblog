using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Service.DTOs.Categories;

namespace WeblogSample.Service.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto> GetByIdAsync(short id);
    Task<short> CreateAsync(CategoryDto dto);
    Task<bool> UpdateAsync(CategoryDto dto);
    Task<bool> DeleteAsync(short id);
}
