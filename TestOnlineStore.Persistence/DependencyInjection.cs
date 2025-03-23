using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestOnlineStore.Persistence.Repositories;
using TestOnlineStore.Persistence.Repositories.Interfaces;

namespace TestOnlineStore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DbConnection");

        services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);

        services.AddDbContext<TestOnlineStoreDBContext>(options =>
            options.UseSqlServer(connection));

        services.AddScoped<IRepositoryProductCategory, RepositoryProductCategory>();
        services.AddScoped<IRepositoryProduct, RepositoryProduct>();

        return services;
    }
}
