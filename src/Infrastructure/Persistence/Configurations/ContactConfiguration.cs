namespace Infrastructure.Persistence.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(128);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(50);
            builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Message).IsRequired();
        }
    }
}
