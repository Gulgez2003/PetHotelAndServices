namespace Infrastructure.Persistence.Configurations
{
    public class AnimalConfiguration : BaseAuditableEntityConfiguration<Animal>
    {
        public override void Configure(EntityTypeBuilder<Animal> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name).HasMaxLength(128).IsRequired();
        }
    }
}
