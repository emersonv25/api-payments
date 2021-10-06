using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models.EntityModel
{
    public static class InstallmentMap
    {
        public static void Map(EntityTypeBuilder<Installment> entityBuilder)
        {
                entityBuilder.ToTable("Parcelas");

                entityBuilder.HasKey(i => i.Id);

                entityBuilder.Property(i => i.Id).HasColumnName("IdParcela").ValueGeneratedOnAdd();
                entityBuilder.Property(i => i.Amount).HasColumnName("ValorBruto").IsRequired().HasColumnType("decimal(8,2)");
                entityBuilder.Property(i => i.NetAmount).HasColumnName("ValorLiquido").IsRequired().HasColumnType("decimal(8,2)");
                entityBuilder.Property(i => i.AnticipatedAmount).HasColumnName("ValorAntecipado").HasColumnType("decimal(8,2)");
                entityBuilder.Property(i => i.InstallmentNumber).HasColumnName("NumeroParcela").IsRequired();
                entityBuilder.Property(i => i.ForecastPaymentAt).HasColumnName("PrevisaoRecebimentoData").IsRequired();
                entityBuilder.Property(i => i.PaymentAt).HasColumnName("RepasseData");
                entityBuilder.Property(i => i.TransactionId).HasColumnName("IdTransacao");
            
        }
    }


}
