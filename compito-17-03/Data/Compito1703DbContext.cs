using Microsoft.EntityFrameworkCore;
using compito_17_03.Models;

namespace compito_17_03.Data
{
    public class Compito1703DbContext : DbContext
    {
        public Compito1703DbContext(DbContextOptions<Compito1703DbContext> options) : base(options) { }

        public DbSet<Studente> Studenti { get; set; }

    }
}
