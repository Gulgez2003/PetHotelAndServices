using FinalProjectApp.Application.Features.Testimonials.Queries.GetAllComments;

namespace FinalProjectApp.Application.Common.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Animal, AnimalGetDTO>();
            CreateMap<Product, ProductGetDTO>();
            CreateMap<SubCategory, SubCategoryGetDTO>();
            CreateMap<Category, CategoryGetDTO>();
            CreateMap<Gallery, GalleryGetDTO>();
            CreateMap<Setting, SettingGetDTO>();
            CreateMap<Service, ServiceGetDTO>();
            CreateMap<Testimonial, TestimonialGetDTO>();
        }
    }
}
