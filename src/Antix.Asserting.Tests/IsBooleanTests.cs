namespace Antix.Asserting.Tests;

public sealed class IsBooleanTests
{
    [Fact]
    public void Happy()
        => Assert.True(
            Validate
                .It(true, it => it.True())
                .It(false, it => it.False())
                .Not.It(true, it => it.False())
            .Ok()
            );

    [Fact]
    public void Fails_True()
    {
        const bool value = false;

        Assert.True(
            Validate
                .It(value, it => it.True())
                .HasErrors(out var actualErrors)
            );

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("value:true", actualError);
    }

    [Fact]
    public void Fails_False()
    {
        const bool value = true;

        Assert.True(
            Validate
                .It(value, it => it.False())
                .HasErrors(out var actualErrors)
            );

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("value:false", actualError);
    }

    [Fact]
    public void Fails_Not_True()
    {
        const bool value = true;

        Assert.True(
            Validate
                .It(value, it => it.Not.True())
                .HasErrors(out var actualErrors)
            );

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("value:not-true", actualError);
    }

    [Fact]
    public void Fails_Not_False()
    {
        const bool value = false;

        Assert.True(
            Validate
                .It(value, it => it.Not.False())
                .HasErrors(out var actualErrors)
            );

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("value:not-false", actualError);
    }
}
