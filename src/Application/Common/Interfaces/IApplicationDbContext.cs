namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Animal> Animals { get; }
        DbSet<Category> Categories { get; }
        DbSet<Contact> Contacts { get; }
        DbSet<Gallery> Galleries { get; }
        DbSet<Product> Products { get; }
        DbSet<Setting> Settings { get; }
        DbSet<SubCategory> SubCategories { get; }
        DbSet<Testimonial> Testimonials { get; }
        DbSet<Service> Services { get; }
        DbSet<ApplicationUser> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
