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
            entityBuilder.ToTable("Transactions");

            entityBuilder.HasKey(t => t.Id);

            entityBuilder.Property(t => t.Id).ValueGeneratedOnAdd();
            entityBuilder.Property(t => t.TransactionAt).IsRequired().HasColumnType("datetime");;
            entityBuilder.Property(t => t.ApprovedAt).HasColumnType("datetime");;
            entityBuilder.Property(t => t.DisapprovedAt).HasColumnType("datetime");;
            entityBuilder.Property(t => t.Anticipated).HasColumnName("Antecipado");
            entityBuilder.Property(t => t.Amount).IsRequired().HasColumnType("decimal(8,2)");
            entityBuilder.Property(t => t.NetAmount).HasColumnType("decimal(8,2)");
            entityBuilder.Property(t => t.Fee).IsRequired().HasColumnType("decimal(8,2)");;
            entityBuilder.Property(t => t.InstallmentsNumber).IsRequired();
            entityBuilder.Property(t => t.LastFourDigitsCard).IsRequired();
        
        }

    }
}
