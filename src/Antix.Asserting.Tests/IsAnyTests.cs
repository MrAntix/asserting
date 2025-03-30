namespace Antix.Asserting.Tests;

public sealed class IsAnyTests
{
    [Fact]
    public void Happy() => Assert.True(
        Validate
            .It(string.Empty, it => it.Not.Null())
            .It(default(string), it => it.Null())
            .Ok()
        );

    [Fact]
    public void Fail_Null()
    {
        const string? value = "";

        Assert.True(
            Validate
                .It(value, it => it.Null())
                .HasError(out var actualError)
            );

        Assert.Equal("value:null", actualError);
    }

    [Fact]
    public void Fail_Not_Null()
    {
        const string? value = null;

        Assert.True(
            Validate
                .It(value, it => it.Not.Null())
                .HasError(out var actualError)
            );

        Assert.Equal("value:not-null", actualError);
    }
}
