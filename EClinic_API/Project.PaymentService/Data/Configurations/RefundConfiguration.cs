using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.PaymentService.Data.Configurations
{
    public class RefundConfiguration : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.HasKey(refund => refund.RefundID);
            builder.Property(refund => refund.RefundID).HasDefaultValueSql("NEWID()").IsRequired();
            builder.Property(refund => refund.RefundTime).IsRequired();
            builder.Property(refund => refund.RefundAmount).IsRequired();
            builder.Property(refund => refund.RefundReason).IsRequired();
            builder.Property(refund => refund.PaymentID).IsRequired();
            builder.ToTable("Refunds");
        }
    }
}
