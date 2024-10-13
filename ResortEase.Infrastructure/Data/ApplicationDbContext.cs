using Microsoft.EntityFrameworkCore;
using ResortEase.Domain.Entities;

namespace ResortEase.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Villa> Villas { get; set; }
    }
}
