using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Data.Entities;
using WeblogSample.Service.DTOs.Roles;

namespace WeblogSample.Service.Mappers.Roles;

public class RoleMapper : IMapper<Role, RoleDto>, ICreateMapper<RoleDto, Role>, IUpdateMapper<RoleDto, Role>
{
    public RoleDto ToDto(Role entity)
    {
        return new RoleDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public Role ToEntity(RoleDto dto)
    {
        return new Role
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }

    public void UpdateEntity(RoleDto dto, Role entity)
    {
        entity.Name = dto.Name;
    }
}
