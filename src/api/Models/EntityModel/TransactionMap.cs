using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.EntityModel
{
    public class TransactionMap : DbContext
    {
        public TransactionMap(EntityTypeBuilder<Transaction> entityBuilder)
        {
            entityBuilder.HasKey(x => x.TransactionId);

            entityBuilder.Property(x => x.TransactionId).IsRequired();
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new TransactionMap(modelBuilder.Entity<Transaction>());

        }

    }
}
