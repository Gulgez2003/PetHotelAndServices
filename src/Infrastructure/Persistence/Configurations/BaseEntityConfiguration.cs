namespace Infrastructure.Persistence.Configurations
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p=>p.IsDeleted).HasDefaultValue(false);
        }
    }
}
