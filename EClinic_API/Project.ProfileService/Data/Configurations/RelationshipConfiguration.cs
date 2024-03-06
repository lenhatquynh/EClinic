using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.ProfileService.Data.Configurations
{
    public class RelationshipConfiguration : IEntityTypeConfiguration<Relationship>
    {
        public void Configure(EntityTypeBuilder<Relationship> builder)
        {
            builder.HasKey(p => p.RelationshipID);
            builder.Property(p => p.RelationshipID).HasDefaultValueSql("NEWID()").IsRequired();
            builder.Property(p => p.RelationshipName).IsRequired();
            builder.HasMany(r => r.HealthProfiles)
                   .WithOne(hp => hp.Relationship)
                   .HasForeignKey(hp => hp.RelationshipID)
                   .HasConstraintName("PK_Relationship_One_To_Many_HealthProfiles")
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new Relationship { RelationshipID = ConstantsData.MyRelationshipID, RelationshipName = ConstantsData.MyRelationshipName });
            builder.ToTable("Relationships");
        }
    }
}
