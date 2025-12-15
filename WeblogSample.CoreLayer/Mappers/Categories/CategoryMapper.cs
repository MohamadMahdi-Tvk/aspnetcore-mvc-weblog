using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Data.Entities;
using WeblogSample.Service.DTOs.Categories;

namespace WeblogSample.Service.Mappers.Categories;

public class CategoryMapper : IMapper<Category, CategoryDto>, ICreateMapper<CategoryDto, Category>, IUpdateMapper<CategoryDto, Category>
{
    public CategoryDto ToDto(Category entity)
    {
        return new CategoryDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public Category ToEntity(CategoryDto dto)
    {
        return new Category
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }

    public void UpdateEntity(CategoryDto dto, Category entity)
    {
        entity.Name = dto.Name;
    }
}
