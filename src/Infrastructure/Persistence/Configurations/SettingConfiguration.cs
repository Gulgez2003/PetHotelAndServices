namespace Infrastructure.Persistence.Configurations
{
    public class SettingConfiguration : BaseAuditableEntityConfiguration<Setting>
    {
        public override void Configure(EntityTypeBuilder<Setting> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.Information).IsRequired().HasMaxLength(256);
            builder.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Address).IsRequired().HasMaxLength(256);
            builder.Property(s => s.FaceBookIcon).IsRequired();
            builder.Property(s => s.TwitterIcon).IsRequired();
            builder.Property(s => s.InstagramIcon).IsRequired();
        }
    }
}
