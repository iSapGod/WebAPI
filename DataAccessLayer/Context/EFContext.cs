using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<Phone> Phones { get; set; }
    }
}
