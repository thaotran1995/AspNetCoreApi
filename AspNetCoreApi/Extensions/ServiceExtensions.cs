﻿using Contracts;
using LoggerService;

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
    }
}
