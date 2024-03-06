using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.CommunicateService.Data.Configurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(c => c.ChatMessageID);
            builder.Property(c => c.ChatMessageID).HasDefaultValueSql("NEWID()").IsRequired();
            builder.Property(c => c.UserID).IsRequired();
            builder.Property(c => c.Content).IsRequired();
            builder.Property(c => c.Type).IsRequired();
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.ToTable("ChatMessages");

        }
    }
}
