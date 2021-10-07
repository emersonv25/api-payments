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
            entityBuilder.ToTable("Transacoes");

            entityBuilder.HasKey(t => t.Id);

            entityBuilder.Property(t => t.Id).HasColumnName("IdTransacao").ValueGeneratedOnAdd();
            entityBuilder.Property(t => t.TransactionAt).HasColumnName("DataTransacao").IsRequired();
            entityBuilder.Property(t => t.ApprovedAt).HasColumnName("DataAprovacao");
            entityBuilder.Property(t => t.DisapprovedAt).HasColumnName("DataReprovacao");
            entityBuilder.Property(t => t.Anticipated).HasColumnName("Antecipado");
            entityBuilder.Property(t => t.Acquirer).HasColumnName("Adquirente");
            entityBuilder.Property(t => t.Amount).HasColumnName("ValorBruto").IsRequired().HasColumnType("decimal(8,2)");
            entityBuilder.Property(t => t.NetAmount).HasColumnName("ValorLiquido").IsRequired().HasColumnType("decimal(8,2)");
            entityBuilder.Property(t => t.Fee).HasColumnName("Taxa").IsRequired().HasColumnType("decimal(8,2)");;
            entityBuilder.Property(t => t.InstallmentsNumber).HasColumnName("NumeroParcelas").IsRequired();
            entityBuilder.Property(t => t.LastFourDigitsCard).HasColumnName("UltimosQuatroDigitos").IsRequired();
        
        }

    }
}
