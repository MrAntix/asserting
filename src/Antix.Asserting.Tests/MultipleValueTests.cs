using static Antix.Asserting.Is;

namespace Antix.Asserting.Tests;

public sealed class MultipleValueTests
{
    [Fact]
    public void Happy()
    {
        var value = new
        {
            Name = "NAME",
            Age = 35
        };

        var values = new[] { value, value };

        var actualErrors = Validate
            .It(value.Name).Is(Length(4))
            .It(value.Age).Is(LessThan(40))
            .It(values).Is(Count(2))
            .Errors;

        Assert.Empty(actualErrors);
    }

    [Fact]
    public void Fails_Multi_Value()
    {
        var value = new
        {
            Name = "NAME",
            Age = 35
        };

        var values = new[] { value, value };

        var actualErrors = Validate
            .It(value.Name).Is(Length(5))
            .It(value.Age).Is(GreaterThan(40))
            .It(values).Is(Count(1))
            .Errors;

        Assert.Equal("value.Name:length(5)", actualErrors[0]);
        Assert.Equal("value.Age:greater-than(40)", actualErrors[1]);
        Assert.Equal("values:count(1)", actualErrors[2]);
    }
}
