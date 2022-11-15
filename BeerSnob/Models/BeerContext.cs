using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BeerSnob.Models
{
    public class BeerContext :DbContext
    {
        public BeerContext(DbContextOptions<BeerContext> options) : base(options) { }

        public DbSet<Beer> Beers { get; set; } = null!;
    }
}
