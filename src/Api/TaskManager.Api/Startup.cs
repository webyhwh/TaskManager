using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Features;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application;
using TaskManager.Infrastructure;
using TaskManager.Persistence;
using TaskManager.Application.Common;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediatR;
using TaskManager.Application.System.Commands.SeedSampleData;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace TaskManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            services.Configure<FormOptions>(x => x.MultipartBodyLengthLimit = 4294967295);

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            services.AddCors(o => o.AddDefaultPolicy(builder =>
            {
                builder.SetIsOriginAllowed(_ => true)
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddPersistence(Configuration);
            services.AddApplication();

            services.AddSwaggerGen(c =>
            {
                c.MapType(typeof(IFormFile), () => new OpenApiSchema() { Type = "file", Format = "binary" });
                c.DescribeAllParametersInCamelCase();

                c.ExampleFilters();

                c.EnableAnnotations();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TaskManager.Api",
                    Description = "API дл€ управлени€ задачами и св€занными файлами",
                    Version = "v1"
                });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "TaskManager.Api.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddScoped<IFileStorage, LocalDiskFileStorage>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilogFileLogging(Configuration);

            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            using var dbScope = app.ApplicationServices.CreateScope();
            var dbContext = dbScope.ServiceProvider.GetRequiredService<TaskManagerDbContext>();
            dbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager.Api v1"));

                using var scope = app.ApplicationServices.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                // seed example data
                await mediator.Send(new SeedSampleDataCommand(), CancellationToken.None);
            }
        }
    }
}
