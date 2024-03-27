using FluentAssertions;
using ReverseAnalytics.Tests.Unit.Domain;

namespace ReverseAnalytics.Tests.Unit.Services;

public class CommonServicesFixture : DomainBaseFixture
{
    [Fact]
    public void Services_ShouldBeSealed()
    {
        var services = GetServices();

        foreach (var service in services)
        {
            service.IsSealed.Should().BeTrue($"Service: {service.Name} should be sealed.");
        }
    }
}
