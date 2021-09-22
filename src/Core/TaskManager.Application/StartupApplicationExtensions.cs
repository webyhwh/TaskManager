using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using TaskManager.Application.Common;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Services;

namespace TaskManager.Application
{
    public static class StartupApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
