namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : BaseAuditableEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Title).IsRequired().HasMaxLength(128);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(256);
            builder.Property(p => p.InStock).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.UnitsInStock).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            //builder.Property(p => p.ProductImages).IsRequired();
        }
    }
}
