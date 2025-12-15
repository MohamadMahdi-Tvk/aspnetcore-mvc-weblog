using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Service.DTOs.Roles;

namespace WeblogSample.Service.Interfaces;

public interface IRoleService
{
    Task<List<RoleDto>> GetAllAsync();
    Task<RoleDto> GetByIdAsync(short id);
    Task<short> CreateAsync(RoleDto dto);
    Task<bool> UpdateAsync(RoleDto dto);
    Task<bool> DeleteAsync(short id);
}
