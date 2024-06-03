namespace Infrastructure.Persistence.Configurations
{
    public abstract class BaseAuditableEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseAuditableEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(p => p.CreatedBy).IsRequired();
            builder.Property(p => p.LastModifiedDate).IsRequired(false);
            builder.Property(p => p.LastModifiedBy).IsRequired(false);
        }
    }
}
