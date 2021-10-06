using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models.EntityModel
{
    public static class InstallmentMap
    {
        public static void Map(EntityTypeBuilder<Installment> entityBuilder)
        {
                entityBuilder.ToTable("Parcelas");

                entityBuilder.HasKey(x => x.InstallmentId);

                entityBuilder.Property(x => x.InstallmentId).ValueGeneratedOnAdd();
                //entityBuilder.HasOne(i => i.Transaction).WithMany(t => t.Installments);
            
        }
    }


}
