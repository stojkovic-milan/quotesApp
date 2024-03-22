using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace QuotesApi.Models;

public class QuotesContext : DbContext
{
    public QuotesContext(DbContextOptions<QuotesContext> options)
        : base(options)
    {
    }


    public DbSet<Rating> Ratings { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Quote> Quotes { get; set; } = null!;

}