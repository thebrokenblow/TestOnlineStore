using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace TestOnlineStore.WebApi.Configurations;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            var apiVersion = description.ApiVersion.ToString();

            options.SwaggerDoc(description.GroupName,
                new OpenApiInfo
                {
                    Version = apiVersion,
                    Title = $"OnlineShop API {apiVersion}",
                    Description = "A simple web service that provides a RESTful API for managing products and product categories.",
                    Contact = new OpenApiContact
                    {
                        Name = " Artem Krasov",
                        Email = "artkrasov@gmail.com",
                        Url = new Uri("https://t.me/krasovart")
                    },
                });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }
    }
}