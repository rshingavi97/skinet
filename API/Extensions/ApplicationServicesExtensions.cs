using Infrastructure.Data;//StoreContext,ProductRepository
using Core.Interfaces; //IProductRepository
using Microsoft.AspNetCore.Mvc;//ApiBehaviorOptions
using Microsoft.EntityFrameworkCore;//UseSqlite
using API.Errors;//ApiValidationErrorResponse
namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<StoreContext>(opt=>{
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<ApiBehaviorOptions>
            ( 
            options=> {
                        options.InvalidModelStateResponseFactory = 
                        actioncontext=>{
                                        var errorList=actioncontext.ModelState.Where(e=>e.Value.Errors.Count>0)
                                                                            .SelectMany(x=>x.Value.Errors)
                                                                            .Select(x=>x.ErrorMessage).ToArray();
                                        var errorResponse=new ApiValidationErrorResponse{Errors=errorList};
                                        return new BadRequestObjectResult(errorResponse);
                                    };          
                    }
            );
            return services;
        }
        
    }
}