using FluentAssertions;
using ReverseAnalytics.Domain.Common;
using System.Reflection;

namespace ReverseAnalytics.Tests.Unit.Domain;

public class EntitiesFixture : DomainBaseFixture
{
    [Fact]
    public void EntitiesShouldInheritFromEntityBase()
    {
        var entities = GetEntities();

        foreach (var entity in entities)
        {
            entity.Should().BeAssignableTo(typeof(BaseEntity));
        }
    }

    [Fact]
    public void AllNavigationPropertiesInEntities_ShouldBeVirtual()
    {
        var entities = GetEntities();
        var properties = entities
            .SelectMany(t => t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
            .Where(p => (p.PropertyType.IsClass || p.PropertyType.IsInterface) && p.PropertyType != typeof(string))
            .ToList();

        Assert.NotEmpty(properties);
        Console.WriteLine(string.Join(", ", properties.Select(p => $"{p.DeclaringType}.{p.Name}")));

        foreach (var property in properties)
        {
            property.GetMethod!.IsVirtual.Should().BeTrue($"{property.DeclaringType}.{property.Name} must be declared as virtual.");
        }
    }

    // TODO make sure that all entities' properties are init only
}
