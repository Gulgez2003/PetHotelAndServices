namespace FinalProjectApp.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(b => b.FullName).IsRequired().HasMaxLength(128);
            builder.Property(b => b.Email).IsRequired().HasMaxLength(256);
            builder.Property(b => b.UserName).IsRequired().HasMaxLength(256);
            builder.Property(b => b.PasswordHash).IsRequired().HasMaxLength(256);
        }
    }
}
