using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Infrastructure.Persistence;
using ReverseAnalytics.Services;

namespace ReverseAnalytics.Tests.Unit.Domain;

public class DomainBaseFixture
{
    protected DomainBaseFixture()
    {
    }

    protected static List<Type> GetServices()
    {
        return typeof(ProductCategoryService).Assembly
            .GetTypes()
            .Where(t => t.Namespace is not null && t.Namespace.Contains("Services"))
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .ToList();
    }

    protected static List<Type> GetEntities()
    {
        return typeof(BaseEntity).Assembly
            .GetTypes()
            .Where(x => x.Namespace != null && x.Namespace.EndsWith("Entities"))
            .ToList();
    }

    protected static List<string> GetMappers()
    {
        return typeof(BaseEntity).Assembly
            .GetTypes()
            .Where(x => !x.IsAbstract && !x.IsInterface && typeof(Profile).IsAssignableFrom(x))
            .Select(x => x.Name)
            .ToList();
    }

    protected static List<string> GetConfigurations()
    {
        return typeof(ApplicationDbContext).Assembly
            .GetTypes()
            .Where(IsEntityTypeConfiguration)
            .Select(x => x.Name)
            .ToList();
    }

    private static bool IsEntityTypeConfiguration(Type type)
    {
        var interfaceType = type.GetInterfaces().ToList();
        return interfaceType.Exists(r => r.IsGenericType && r.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
    }
}
