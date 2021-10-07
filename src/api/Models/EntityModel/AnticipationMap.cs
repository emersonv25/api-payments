using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models.EntityModel
{
    public static class AnticipationMap
    {
        public static void Map(EntityTypeBuilder<Anticipation> entityBuilder)
        {
                entityBuilder.ToTable("Anticipation");

                entityBuilder.HasKey(i => i.Id);
                entityBuilder.Property(i => i.Id).ValueGeneratedOnAdd();
                entityBuilder.Property(i => i.RequestAt).IsRequired().HasColumnType("datetime");
                entityBuilder.Property(i => i.StartAt).HasColumnType("datetime");
                entityBuilder.Property(i => i.EndAt).HasColumnType("datetime");
                entityBuilder.Property(i => i.AmountRequest).IsRequired().HasColumnType("decimal(8,2)");
                entityBuilder.Property(i => i.AmountApproved).HasColumnType("decimal(8,2)");
            
        }
    }


}
