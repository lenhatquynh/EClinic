using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.CommunicateService.Data.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.RoomID);
            builder.Property(r => r.RoomID).HasDefaultValueSql("NEWID()").IsRequired();
            builder.Property(r => r.IsClosed).IsRequired();
            builder.Property(r => r.ReceiverID).IsRequired();
            builder.Property(r => r.SenderID).IsRequired();
            builder.Property(r => r.CreatedAt).IsRequired();
            builder.HasMany(r => r.ChatMessages)
                .WithOne(c => c.Room)
                .HasForeignKey(c => c.RoomID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("PK_ChatRoom_Many_To_One_ChatMessage");
            builder.ToTable("Rooms");
        }
    }
}
