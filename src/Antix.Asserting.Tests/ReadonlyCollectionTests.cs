using System.Collections.Generic;
using static Antix.Asserting.Is;

namespace Antix.Asserting.Tests;

public sealed class ReadonlyCollectionTests
{
    [Theory]
    [InlineData(new[] { 1, 2 })]
    public void Happy(IReadOnlyCollection<int>? value)
    {
        var actualErrors = Validate
            .It(value)
                .Is(Count(2), MinCount(2), MaxCount(2))
                .Contains(1)
            .It(value).Is(MinCount(1))
            .Run();

        Assert.Empty(actualErrors);
    }

    [Fact]
    public void Fails_Not_Contains()
    {
        var value = new[] { 1, 2 };
        var actualErrors = Validate
            .It(value).Not.Contains(2)
            .Run();

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("value:not-contains", actualError);
    }

    [Fact]
    public void Fails_Not_Contains_Predicate()
    {
        var value = new[] { 1, 2 };
        var actualErrors = Validate
            .It(value).Not.Contains(i => i == 2)
            .Run();

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("value:not-contains", actualError);
    }

    [Theory]
    [InlineData(new[] { 1, 2 }, "value:count(5)")]
    public void Fails_Count(IReadOnlyCollection<int>? value, string? expectedError)
    {
        var actualErrors = Validate
            .It(value).Is(Count(5))
            .Run();

        var actualError = Assert.Single(actualErrors);
        Assert.Equal(expectedError, actualError);
    }

    [Theory]
    [InlineData(new[] { 1, 2 }, "value:min-count(5)")]
    public void Fails_MinCount(IReadOnlyCollection<int>? value, string? expectedError)
    {
        var actualErrors = Validate
            .It(value).Is(MinCount(5))
            .Run();

        var actualError = Assert.Single(actualErrors);
        Assert.Equal(expectedError, actualError);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, "value:max-count(5)")]
    public void Fails_MaxCount(IReadOnlyCollection<int>? value, string? expectedError)
    {
        var actualErrors = Validate
            .It(value).Is(MaxCount(5))
            .Run();

        var actualError = Assert.Single(actualErrors);
        Assert.Equal(expectedError, actualError);
    }
}
