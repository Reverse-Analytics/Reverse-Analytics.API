﻿using FluentAssertions;
using Reverse_Analytics.Api.Controllers;
using ReverseAnalytics.Services;
using System.Reflection;

namespace ReverseAnalytics.Tests.Unit;

public class ConstructorsFixture
{
    [Fact]
    public void ConstructorsOfAllControllers_ShouldThrowArgumentNullException_WhenAnyParameterIsNull()
    {
        // Arrange
        var constructors = typeof(ProductCategoryController).Assembly
            .GetTypes()
            .Where(t => t.Namespace is not null && t.Namespace.Contains("Controllers"))
            .SelectMany(t => t.GetConstructors())
            .Where(t => t.GetParameters().Length > 0)
            .ToList();

        // Act & Assert
        foreach (var constructor in constructors)
        {
            var parameters = constructor.GetParameters();
            var nullArgs = new object[parameters.Length];
            var ex = Assert.Throws<TargetInvocationException>(() => constructor.Invoke(nullArgs));
            var innerException = ex.InnerException;

            Assert.NotNull(innerException);
            Assert.IsType<ArgumentNullException>(innerException);

            var paramName = (innerException as ArgumentNullException)?.ParamName;
            Assert.Contains(paramName, parameters.Select(p => p.Name));
        }
    }

    [Fact]
    public void ConstructorsOfAllCoreServices_ShouldThrowArgumentNullException_WhenAnyParameterIsNull()
    {
        // Arrange
        var constructors = typeof(ProductCategoryService).Assembly
            .GetTypes()
            .Where(t => t.Namespace is not null && t.Namespace.Contains("Services"))
            .SelectMany(t => t.GetConstructors())
            .Where(t => t.GetParameters().Length > 0)
            .ToList();

        // Act & Assert
        foreach (var constructor in constructors)
        {
            var parameters = constructor.GetParameters();
            var nullArgs = new object[parameters.Length];
            var ex = Assert.Throws<TargetInvocationException>(() => constructor.Invoke(nullArgs));
            var innerException = ex.InnerException;

            innerException.Should().NotBeNull();
            innerException.Should().BeAssignableTo<ArgumentNullException>();

            var paramName = (innerException as ArgumentNullException)?.ParamName;
            parameters.Should().Contain(p => p.Name == paramName);
        }
    }
}
