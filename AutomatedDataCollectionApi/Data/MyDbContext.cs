using Microsoft.EntityFrameworkCore;
using AutomatedDataCollectionApi.Models;

namespace AutomatedDataCollectionApi.Data
{
    public class MyDbContext : DbContext
{
    public DbSet<UrlEntity> Urls { get; set; } // Use Urls here
    public object? UrlEntities { get; internal set; }
    public DbSet<ParsedEntity> ParsedData { get; set; } // Use ParsedData here\
    public object? ParsedEntities { get; internal set; }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }
}
}