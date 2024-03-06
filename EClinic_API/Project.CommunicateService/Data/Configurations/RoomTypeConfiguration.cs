using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.CommunicateService.Data.Configurations
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.HasKey(r => r.RoomTypeID);
            builder.Property(r => r.RoomTypeID).HasDefaultValueSql("NEWID()").IsRequired();
            builder.Property(r => r.RoomTypeName).IsRequired();
            builder.HasMany(r => r.Rooms)
                .WithOne(c => c.RoomType)
                .HasForeignKey(c => c.RoomTypeID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PK_RoomType_Many_To_One_Room");
            builder.HasData(
                            new RoomType { RoomTypeID = ConstantsData.DoctorRoomTypeID, RoomTypeName = ConstantsData.DoctorRoomTypeName },
                            new RoomType { RoomTypeID = ConstantsData.SupporterRoomTypeID, RoomTypeName = ConstantsData.SupporterRoomTypeName }
                            );
            builder.ToTable("RoomTypes");
        }
    }
}
