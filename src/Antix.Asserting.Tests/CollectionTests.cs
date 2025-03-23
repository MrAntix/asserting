using static Antix.Asserting.Is;

namespace Antix.Asserting.Tests;

public sealed class CollectionTests
{
    [Fact]
    public void Happy()
    {
        var values = new[] { 1, 2 };
        var actualErrors = Validate
            .It(values)
                .Is(NotNull, Count(2), MinCount(1), MaxCount(3))
                .Is(All(GreaterThan(0)))
                .Is(MinCount(1, EqualTo(2)))
                .Is(MaxCount(1, EqualTo(1)))
            .Errors;

        Assert.Empty(actualErrors);
    }

    [Fact]
    public void Fails_Count()
    {
        var values = new[] { 1, 2 };
        Assert.True(
            Validate
                .It(values)
                    .Is(NotNull, Count(10))
                .Failure(out var actualErrors)
            );

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("values:count(10)", actualError);
    }

    [Fact]
    public void Fails_All()
    {
        var values = new[] { 2, 1 };
        var actualErrors = Validate
            .It(values).Is(All(GreaterThan(1)))
            .Errors;

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("values:all(greater-than(1))", actualError);
    }

    [Fact]
    public void Fails_Not_All()
    {
        var values = new[] { 2, 1 };
        var actualErrors = Validate
            .It(values).Not.Is(All(GreaterThan(0)))
            .Errors;

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("values:not-all(greater-than(0))", actualError);
    }

    [Fact]
    public void Fails_Any()
    {
        var values = new[] { 2, 1 };
        var actualErrors = Validate
            .It(values).Is(Any(GreaterThan(2)))
            .Errors;

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("values:any(greater-than(2))", actualError);
    }

    [Fact]
    public void Fails_minCount_EqualTo()
    {
        var values = new[] { 2, 1 };
        var actualErrors = Validate
            .It(values).Is(MinCount(2, EqualTo(3)))
            .Errors;

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("values:min-count(2,equal-to(3))", actualError);
    }

    [Fact]
    public void Fails_Any_EqualTo()
    {
        var values = new[] { 1, 2 };
        Assert.True(
            Validate
                .It(values)
                    .Not.Is(Any(EqualTo(1)))
                .Failure(out var actualErrors)
            );

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("values:not-any(equal-to(1))", actualError);
    }
}
