using ReverseAnalytics.Tests.Unit.Domain;

namespace ReverseAnalytics.Tests.Unit.Services;

public class CommonServicesFixture : DomainBaseFixture
{
    [Fact]
    public void Services_ShouldNotContain_NonPrivateMembersExceptConstructorsAndInterfaces()
    {
        var services = GetServices();

        foreach (var service in services)
        {
            var methods = service.GetMembers().ToList();
            var basea = service.GetInterfaces();
            var metds = basea.SelectMany(s => s.GetMethods());
            var mm = service.GetMethods();


        }
    }

    [Fact]
    public void Services_ShouldBeSealed()
    {

    }
}
