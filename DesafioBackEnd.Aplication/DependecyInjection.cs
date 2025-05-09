
using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockManager.Aplication.Behavior;
using StockManager.Aplication.JWTRepository;

namespace StockManager.Aplication
{
    public static class DependecyInjection
    {
        public static void AddAplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtKey>(configuration.GetSection("JwtKeyConfiguration"));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependecyInjection).Assembly));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));
        }
    }
}
