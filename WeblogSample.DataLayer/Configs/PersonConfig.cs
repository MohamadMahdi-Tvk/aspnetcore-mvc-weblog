using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Data.Entities;

namespace WeblogSample.Data.Configs;

public class PersonConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(p => p.UserName).IsRequired().HasMaxLength(50);
        builder.Property(p => p.PasswordHash).IsRequired();

        builder.HasOne(p => p.Role)
        .WithMany(r => r.People)
        .HasForeignKey(p => p.RoleId)
        .OnDelete(DeleteBehavior.SetNull);

        builder.HasData(new Person
        {
            Id = 1,
            RoleId = 1,
            UserName = "Admin",
            PasswordHash = "LBadsUy90TkCrGfhT6MZp30v718MVofKWVrEdfAsf0cuo2X5QcI+oHe4POT37cmx", //123456    
            IsActive = true,
            InsertDate = new DateTime(2025, 12, 15, 0, 0, 0),
            UpdateDate = new DateTime(2025, 12, 15, 0, 0, 0)
        });
    }
}
