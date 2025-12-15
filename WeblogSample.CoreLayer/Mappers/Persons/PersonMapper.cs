using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using WeblogSample.Data.Entities;
using WeblogSample.Service.DTOs.Persons;

namespace WeblogSample.Service.Mappers.Persons;

public class PersonMapper : IMapper<Person, PersonDto>, ICreateMapper<PersonCreateDto, Person>, IUpdateMapper<PersonUpdateDto, Person>
{
    public PersonDto ToDto(Person entity)
    {
        return new PersonDto
        {
            Id = entity.Id,
            UserName = entity.UserName,
            RoleId = entity.Role.Id,
            IsActive = entity.IsActive
        };
    }

    public Person ToEntity(PersonCreateDto dto)
    {
        return new Person
        {
            UserName = dto.UserName,
            PasswordHash = HashPassword(dto.Password),
            RoleId = dto.RoleId,
            InsertDate = DateTime.Now,
            IsActive = true
        };
    }

    public void UpdateEntity(PersonUpdateDto dto, Person entity)
    {
        entity.UserName = dto.UserName;
        entity.RoleId = dto.RoleId;
        entity.UpdateDate = DateTime.Now;
    }

    private string HashPassword(string password)
    {
        using var deriveBytes = new Rfc2898DeriveBytes(password, 16, 10000, HashAlgorithmName.SHA256);
        var salt = deriveBytes.Salt;
        var key = deriveBytes.GetBytes(32);
        return Convert.ToBase64String(salt.Concat(key).ToArray());
    }
}
