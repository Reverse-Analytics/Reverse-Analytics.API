using AutoFixture;
using AutoMapper;
using Moq;
using ReverseAnalytics.Domain.Interfaces.Repositories;

namespace ReverseAnalytics.Tests.Unit.Services;
public abstract class ServiceFixtureBase
{
    protected readonly Mock<ICommonRepository> _mockRepository;
    protected readonly Mock<IMapper> _mockMapper;
    protected readonly Fixture _fixture;

    protected ServiceFixtureBase()
    {
        _mockRepository = new Mock<ICommonRepository>();
        _mockMapper = new Mock<IMapper>();
        _fixture = CreateFixture();
    }

    private static Fixture CreateFixture()
    {
        var fixture = new Fixture();
        fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        return fixture;
    }
}
