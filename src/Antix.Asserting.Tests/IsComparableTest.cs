namespace Antix.Asserting.Tests;

public sealed class IsComparableTest
{
    [Fact]
    public void Happy()
        => Assert.True(
            Validate
                .It(1, it =>
                    it.EqualTo(1)
                    && it.GreaterOrEqualTo(1)
                    && it.LessOrEqualTo(1)
                    && it.GreaterThan(0)
                    && it.LessThan(2)
                )
            .Ok()
            );

    [Fact]
    public void Fail_EqualTo()
    {
        const int value = 1;

        Assert.True(
            Validate
                .It(value, it => it.EqualTo(2))
                .HasError(out var actualError)
            );

        Assert.Equal("value:equal-to(2)", actualError);
    }

    [Fact]
    public void Fail_GreaterOrEqualTo()
    {
        const int value = 1;

        Assert.True(
            Validate
                .It(value, it => it.GreaterOrEqualTo(2))
                .HasError(out var actualError)
            );

        Assert.Equal("value:greater-or-equal-to(2)", actualError);
    }

    [Fact]
    public void Fail_GreaterThan()
    {
        const int value = 1;

        Assert.True(
            Validate
                .It(value, it => it.GreaterThan(2))
                .HasError(out var actualError)
            );

        Assert.Equal("value:greater-than(2)", actualError);
    }

    [Fact]
    public void Fail_LessOrEqualTo()
    {
        const int value = 1;

        Assert.True(
            Validate
                .It(value, it => it.LessOrEqualTo(0))
                .HasError(out var actualError)
            );

        Assert.Equal("value:less-or-equal-to(0)", actualError);
    }

    [Fact]
    public void Fail_LessThan()
    {
        const int value = 1;

        Assert.True(
            Validate
                .It(value, it => it.LessThan(0))
                .HasError(out var actualError)
            );

        Assert.Equal("value:less-than(0)", actualError);
    }
}
