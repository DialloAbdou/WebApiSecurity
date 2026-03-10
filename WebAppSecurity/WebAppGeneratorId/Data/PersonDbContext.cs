using Microsoft.EntityFrameworkCore;
using WebAppGeneratorId.Data.Model;

namespace WebAppGeneratorId.Data
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> persons { get; set; }

        public PersonDbContext(DbContextOptions<PersonDbContext> options)
            : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(c =>
            {
                c.ToTable("Persons");
                c.Property(p=>p.Name)
                .IsRequired().HasMaxLength(256);
                c.Property(p => p.LastName).HasMaxLength(256);
                c.Property(p => p.DisplayId)
                .HasMaxLength(16);
                c.HasIndex(p=>p.DisplayId).IsUnique();

            });
        }
    }
}
