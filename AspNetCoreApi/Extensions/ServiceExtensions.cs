using Contracts;
using Entities;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace AspNetCoreApi.Extensions
{
    public static class ServiceExtensions
    {
        // 01- extension AddCors Service
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        
        // 02- Extension ConfigureIISintergration service
        public static void ConfigureIISIntergartion(this IServiceCollection services)
        {

        }

        // 03- Configure Logger Service
        public static void ConfigureLoggerService(this IServiceCollection services) 
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        // 04- Configure Mysql context
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mysqlconnection:connectionString"];

            services.AddDbContext<RepositoryContext>(options => options.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion));
        }

        //05- Add configure repository wrapper
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
