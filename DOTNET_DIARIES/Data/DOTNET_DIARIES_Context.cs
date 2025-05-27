using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DOTNET_DIARIES.Models;

namespace DOTNET_DIARIES.Data;

public class DOTNET_DIARIES_Context : IdentityDbContext
{
    public DOTNET_DIARIES_Context(DbContextOptions<DOTNET_DIARIES_Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogpostTag>()
        .HasKey(bt => new { bt.BlogpostId, bt.TagId });

        modelBuilder.Entity<BlogpostTag>()
            .HasOne(bt => bt.Blogpost)
            .WithMany(b => b.BlogpostTags)
            .HasForeignKey(bt => bt.BlogpostId);

        modelBuilder.Entity<BlogpostTag>()
            .HasOne(bt => bt.Tag)
            .WithMany(t => t.BlogpostTags)
            .HasForeignKey(bt => bt.TagId);

        modelBuilder.Entity<Blogpost>().HasData(new Blogpost
        {
            Id = 1,
            Title = "Blogpost about C#",
            ImageUrl = "https://placecats.com/200/300",
            Content = "This is the content of the first blogpost.",
            PostedDate = new DateTime(2025, 05, 14),
        });

        modelBuilder.Entity<Tag>().HasData(new Tag
        {
            Id = 1,
            Name = "C#"
        }, new Tag
        {
            Id = 2,
            Name = "ASP.NET"
        });

        modelBuilder.Entity<BlogpostTag>().HasData(new BlogpostTag
        {
            BlogpostId = 1,
            TagId = 1
        },
        new BlogpostTag
        {
            BlogpostId = 1,
            TagId = 2
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Blogpost> Blogposts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<BlogpostTag> BlogpostTags { get; set; }

    
}
