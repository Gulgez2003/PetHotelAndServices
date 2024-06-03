namespace Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : BaseAuditableEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(128);
        }
    }
}
