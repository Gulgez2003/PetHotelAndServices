namespace Infrastructure.Persistence.Configurations
{
    public class SubCategoryConfiguration : BaseAuditableEntityConfiguration<SubCategory>
    {
        public override void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(128);
        }
    }
}
