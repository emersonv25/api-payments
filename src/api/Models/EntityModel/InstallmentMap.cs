using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models.EntityModel
{
    public static class InstallmentMap
    {
        public static void Map(EntityTypeBuilder<Installment> entityBuilder)
        {
                entityBuilder.ToTable("Installments");

                entityBuilder.HasKey(i => i.Id);

                entityBuilder.Property(i => i.Id).ValueGeneratedOnAdd();
                entityBuilder.Property(i => i.Amount).IsRequired().HasColumnType("decimal(8,2)");
                entityBuilder.Property(i => i.NetAmount).IsRequired().HasColumnType("decimal(8,2)");
                entityBuilder.Property(i => i.AnticipatedAmount).HasColumnType("decimal(8,2)");
                entityBuilder.Property(i => i.InstallmentNumber).IsRequired();
                entityBuilder.Property(i => i.ForecastPaymentAt).IsRequired().HasColumnType("datetime");
            
        }
    }


}
