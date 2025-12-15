using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using WeblogSample.Data.Entities;

namespace WeblogSample.Data.Configs;

public class CommentConfig : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(p => p.Text).IsRequired().HasMaxLength(500);

        builder.HasOne(c => c.Parent)
        .WithMany(c => c.Replies)
        .HasForeignKey(c => c.ParentId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Person)
        .WithMany(p => p.Comments)
        .HasForeignKey(c => c.PersonId)
        .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(c => c.Article)
        .WithMany(a => a.Comments)
        .HasForeignKey(c => c.ArticleId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}