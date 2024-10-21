using Microsoft.EntityFrameworkCore;
using SigmaTask.Data.Entities;

namespace SigmaTask.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Candidate>(x =>
        {
            x.Property(x => x.Email).IsRequired();
            x.HasKey(x => x.Email);

            x.Property(x => x.FirstName).IsRequired();
            x.Property(x => x.LastName).IsRequired();
            x.Property(x => x.Comment).IsRequired();
        });
    }

    public DbSet<Candidate> Candidates { get; set; }
}
