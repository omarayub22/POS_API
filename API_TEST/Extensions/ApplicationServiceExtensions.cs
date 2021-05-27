using API_TEST.Data;
using API_TEST.Interfaces;
using API_TEST.Repositories;
using API_TEST.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API_TEST.Extensions
{
   public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductRepository,ProductRepository>();
           services.AddDbContext<DataContext>(Options => Options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
} 