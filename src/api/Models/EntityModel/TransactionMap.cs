using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.EntityModel
{
    public static class TransactionMap
    {
        public static void Map(EntityTypeBuilder<Transaction> entityBuilder)
        {
            entityBuilder.ToTable("Transações");

            entityBuilder.HasKey(x => x.TransactionId);

            entityBuilder.Property(x => x.TransactionId).ValueGeneratedOnAdd();
        
        }

    }
}
