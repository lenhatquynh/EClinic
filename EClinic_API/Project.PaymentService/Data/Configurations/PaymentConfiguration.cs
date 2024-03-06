using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.PaymentService.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(payment => payment.PaymentID);
            builder.Property(payment => payment.PaymentID).HasDefaultValueSql("NEWID()").IsRequired();
            builder.Property(payment => payment.TransactionID).IsRequired();
            builder.Property(payment => payment.OrderID).IsRequired();
            builder.Property(payment => payment.PaymentAmount).IsRequired();
            builder.Property(payment => payment.UserID).IsRequired();
            builder.Property(payment => payment.BookingID).IsRequired();
            builder.Property(payment => payment.PaymentTime).IsRequired();
            builder.Property(payment => payment.PaymentService).IsRequired();
            builder.HasOne(payment => payment.Refund)
                   .WithOne(refund => refund.Payment)
                   .HasForeignKey<Refund>(refund => refund.PaymentID)
                   .HasConstraintName("PK_Payment_One_To_One_Refund")
                   .OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Payments");

        }
    }
}
