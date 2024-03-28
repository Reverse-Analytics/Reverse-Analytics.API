using FluentAssertions;

namespace ReverseAnalytics.Tests.Unit.Domain;

public class EntityConfigurationsFixture : DomainBaseFixture
{
    [Fact]
    public void Entities_ShouldHave_Configuration()
    {
        // Arrange
        var expectedEntities = GetEntities();
        var actualConfigurations = GetConfigurations();

        // Assert
        expectedEntities.Count.Should().Be(actualConfigurations.Count);
    }

    [Fact]
    public void ConfigurationNames_ShouldStart_WithEntityNames()
    {
        // Arrange
        var entities = GetEntities();
        var configurations = GetConfigurations();

        // Act & Assert
        foreach (var entity in entities)
        {
            configurations.Should().Contain(configuration => configuration.StartsWith(entity.Name));
        }
    }

    [Fact]
    public void ConfigurationNames_ShouldEnd_WithConfiguration()
    {
        // Arrange
        var configurations = GetConfigurations();

        // Act & Assert
        foreach (var configuration in configurations)
        {
            configuration.Should().EndWith("Configuration");
        }
    }
}
