namespace FinalProjectApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ISender, Mediator>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddAutoMapper(typeof(Mapper).Assembly, Assembly.GetExecutingAssembly());
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });

            services.AddTransient<IRequestHandler<GetAnimalsQuery, List<AnimalGetDTO>>, GetAnimalsQueryHandler>();
            services.AddTransient<IRequestHandler<GetAnimalByIdQuery, AnimalGetDTO>, GetAnimalByIdQueryHandler>();
            services.AddTransient<IRequestHandler<CreateAnimalCommand>, CreateAnimalCommandHandler>();
            services.AddTransient<IValidator<CreateAnimalCommand>, CreateAnimalCommandValidator>();
            services.AddTransient<IRequestHandler<UpdateAnimalCommand>, UpdateAnimalCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteAnimalCommand>, DeleteAnimalCommandHandler>();
            services.AddTransient<IValidator<UpdateAnimalCommand>, UpdateAnimalCommandValidator>();

            services.AddTransient<IRequestHandler<GetCategoriesQuery, List<CategoryGetDTO>>, GetCategoriesQueryHandler>();
            services.AddTransient<IRequestHandler<GetCategoryByIdQuery, CategoryGetDTO>, GetCategoryByIdQueryHandler>();
            services.AddTransient<IRequestHandler<CreateCategoryCommand>, CreateCategoryCommandHandler>();
            services.AddTransient<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
            services.AddTransient<IRequestHandler<UpdateCategoryCommand>, UpdateCategoryCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteCategoryCommand>, DeleteCategoryCommandHandler>();
            services.AddTransient<IValidator<UpdateCategoryCommand>, UpdateCategoryCommandValidator>();

            services.AddTransient<IRequestHandler<GetSubCategoriesQuery, List<SubCategoryGetDTO>>, GetSubCategoriesQueryHandler>();
            services.AddTransient<IRequestHandler<GetSubCategoryByIdQuery, SubCategoryGetDTO>, GetSubCategoryByIdQueryHandler>();
            services.AddTransient<IRequestHandler<CreateSubCategoryCommand>, CreateSubCategoryCommandHandler>();
            services.AddTransient<IValidator<CreateSubCategoryCommand>, CreateSubCategoryCommandValidator>();
            services.AddTransient<IRequestHandler<UpdateSubCategoryCommand>, UpdateSubCategoryCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteSubCategoryCommand>, DeleteSubCategoryCommandHandler>();
            services.AddTransient<IValidator<UpdateSubCategoryCommand>, UpdateSubCategoryCommandValidator>();

            services.AddTransient<IRequestHandler<GetProductsQuery, List<ProductGetDTO>>, GetProductsQueryHandler>();
            services.AddTransient<IRequestHandler<GetProductsWithPaginationQuery, List<ProductGetDTO>>, GetProductsWithPaginationQueryHandler>();
            services.AddTransient<IRequestHandler<GetSearchResultsQuery, List<ProductGetDTO>>, GetSearchResultsQueryHandler>();
            services.AddTransient<IRequestHandler<GetProductByIdQuery, ProductGetDTO>, GetProductByIdQueryHandler>();
            services.AddTransient<IRequestHandler<CreateProductCommand>, CreateProductCommandHandler>();
            services.AddTransient<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            services.AddTransient<IRequestHandler<UpdateProductCommand>, UpdateProductCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteProductCommand>, DeleteProductCommandHandler>();
            services.AddTransient<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();

            services.AddTransient<IRequestHandler<GetCommentsQuery, List<TestimonialGetDTO>>, GetCommentQueryHandler>();
            services.AddTransient<IRequestHandler<GetPublishedCommentsQuery, List<TestimonialGetDTO>>, GetPublishedCommentsQueryHandler>();
            services.AddTransient<IRequestHandler<GetPublishedCommentsByIdQuery, List<TestimonialGetDTO>>, GetPublishedCommentsByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetRejectedCommentsQuery, List<TestimonialGetDTO>>, GetRejectedCommentsQueryHandler>();
            services.AddTransient<IRequestHandler<CreateCommentCommand>, CreateCommentCommandHandler>();
            services.AddTransient<IValidator<CreateCommentCommand>, CreateCommentCommandValidator>();
            services.AddTransient<IRequestHandler<ApproveCommentCommand>, ApproveCommentCommandHandler>();
            services.AddTransient<IRequestHandler<DisapproveCommentCommand>, DisapproveCommentCommandHandler>();

            services.AddTransient<IRequestHandler<GetServicesQuery, List<ServiceGetDTO>>, GetServicesQueryHandler>();
            services.AddTransient<IRequestHandler<GetServiceByIdQuery, ServiceGetDTO>, GetServiceByIdQueryHandler>();
            services.AddTransient<IRequestHandler<CreateServiceCommand>, CreateServiceCommandHandler>();
            services.AddTransient<IValidator<CreateServiceCommand>, CreateServiceCommandValidator>();
            services.AddTransient<IRequestHandler<UpdateServiceCommand>, UpdateServiceCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteServiceCommand>, DeleteServiceCommandHandler>();
            services.AddTransient<IValidator<UpdateServiceCommand>, UpdateServiceCommandValidator>();

            services.AddTransient<IRequestHandler<CreateProductImageCommand, List<ProductImage>>, CreateProductImageCommandHandler>(); 
            services.AddTransient<IRequestHandler<CreateAnimalImageCommand, AnimalImage>, CreateAnimalImageCommandHandler>();

            services.AddTransient<IRequestHandler<GetSettingQuery, SettingGetDTO>, GetSettingQueryHandler>();
            services.AddTransient<IRequestHandler<UpdateSettingCommand>, UpdateSettingCommandHandler>();
            services.AddTransient<IValidator<UpdateSettingCommand>, UpdateSettingCommandValidator>();

            services.AddTransient<IRequestHandler<RegisterUserCommand, UserCommandResult>, RegisterUserCommandHandler>();
            services.AddTransient<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();

            services.AddTransient<IRequestHandler<LoginUserCommand, UserCommandResult>, LoginUserCommandHandler>();
            services.AddTransient<IValidator<LoginUserCommand>, LoginUserCommandValidator>();

            services.AddTransient<IRequestHandler<CreateContactCommand>, CreateContactCommandHandler>();
            services.AddTransient<IValidator<CreateContactCommand>, CreateContactCommandValidator>();

            return services;
        }
    }
}
