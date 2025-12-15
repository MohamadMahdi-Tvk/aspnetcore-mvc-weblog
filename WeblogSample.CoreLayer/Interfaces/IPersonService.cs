using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Service.DTOs.Persons;

namespace WeblogSample.Service.Interfaces;

public interface IPersonService
{
    Task<List<PersonDto>> GetAllAsync();
    Task<PersonDto> GetByIdAsync(long id);
    Task<long> CreateAsync(PersonCreateDto dto);
    Task<bool> UpdateAsync(PersonUpdateDto dto);
    Task ToggleActiveAsync(long id);
}
