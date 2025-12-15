using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Data.Entities;

namespace WeblogSample.Data.Configs;

public class ArticleConfig : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.Property(p => p.Title).HasMaxLength(30).IsRequired();
        builder.Property(p => p.ShortDescription).HasMaxLength(500).IsRequired();

        builder.HasOne(a => a.Person)
        .WithMany(p => p.Articles)
        .HasForeignKey(a => a.PersonId)
        .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.Category)
        .WithMany(c => c.Articles)
        .HasForeignKey(a => a.CategoryId)
        .OnDelete(DeleteBehavior.SetNull);
    }
}