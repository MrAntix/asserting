namespace Antix.Asserting.Tests;

public sealed class IsStringTests
{
    [Fact]
    public void Happy()
        => Assert.Empty(
            Validate
                .It("VALUE", it
                    => it.Not.Whitespace()
                    && it.Length(5)
                    && it.LengthGreaterOrEqualTo(5)
                    && it.CountOf(1, 'L')
                    && it.StartsWith("VAL")
                    && it.EndsWith("LUE")
                    && it.Contains("ALU")
                )
                .Errors
            );

    [Fact]
    public void Fail_Length()
    {
        const string value = "VALUE";

        Assert.True(
            Validate
                .It(value, it => it.Length(1))
                .HasError(out var actualError)
            );

        Assert.Equal("value:length(1)", actualError);
    }

    [Fact]
    public void Fail_LengthGreaterThan()
    {
        const string value = "VALUE";

        Assert.True(
            Validate
                .It(value, it => it.LengthGreaterThan(5))
                .HasError(out var actualError)
            );

        Assert.Equal("value:length-greater-than(5)", actualError);
    }

    [Fact]
    public void Fail_LengthGreaterOrEqualTo()
    {
        const string value = "VALUE";

        Assert.True(
            Validate
                .It(value, it => it.LengthGreaterOrEqualTo(6))
                .HasError(out var actualError)
            );

        Assert.Equal("value:length-greater-or-equal-to(6)", actualError);
    }
}
