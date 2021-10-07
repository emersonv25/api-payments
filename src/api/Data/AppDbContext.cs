using api.Models.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Anticipation> Anticipations { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //modelBuilder.Entity<Installment>().HasOne(i => i.Transaction).WithMany(t => t.Installments);
            InstallmentMap.Map(modelBuilder.Entity<Installment>());
            TransactionMap.Map(modelBuilder.Entity<Transaction>());
            AnticipationMap.Map(modelBuilder.Entity<Anticipation>());

        }

    }
}
