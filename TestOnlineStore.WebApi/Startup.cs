using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using FluentValidation;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Encodings.Web;
using TestOnlineStore.Persistence;
using TestOnlineStore.WebApi.Configurations;
using TestOnlineStore.WebApi.Middleware;

namespace TestOnlineStore.WebApi;

public class Startup(IConfiguration configuration, IWebHostEnvironment env)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddPersistence(configuration);
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("X-Version"));
        })
       .AddMvc()
       .AddApiExplorer(options =>
       {
           options.GroupNameFormat = "'v'VVV";
           options.SubstituteApiVersionInUrl = true;
           options.AddApiVersionParametersWhenVersionNeutral = true;
       });
    }

    public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                config.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        app.UseCustomExceptionHandler();

        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}