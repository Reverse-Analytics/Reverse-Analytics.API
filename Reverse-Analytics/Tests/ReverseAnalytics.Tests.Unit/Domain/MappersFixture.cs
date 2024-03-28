using FluentAssertions;

namespace ReverseAnalytics.Tests.Unit.Domain;

public class MappersFixture : DomainBaseFixture
{
    [Fact]
    public void AllEntities_ShouldHave_Mappings()
    {
        var mappers = GetMappers();
        var entities = GetEntities();

        mappers.Count.Should().Be(entities.Count);
    }

    [Fact]
    public void Mappings_ShouldStartWith_EntityNames()
    {
        var mappers = GetMappers();
        var entities = GetEntities();

        foreach (var entity in entities)
        {
            mappers.Should().Contain(mapper => mapper.StartsWith(entity.Name));
        }
    }
}
