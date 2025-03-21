using static Antix.Asserting.Is;
namespace Antix.Asserting.Tests;

public sealed class StringTests
{
    [Fact]
    public void Happy()
    {
        Assert.False(
            Validate
                .It("Hello")
                    .Not.Is(Null, Empty, Whitespace)
                    .Is(MinLength(2), MaxLength(10))
                .Fails(out var actualErrors)
            );

        Assert.Empty(actualErrors);
    }

    [Theory]
    [InlineData(null, "value:not-null")]
    [InlineData("", "value:not-empty")]
    [InlineData(" ", "value:not-whitespace")]
    public void Fail_Null_Empty_Whitespace(string? value, string? expectedError)
    {
        var actualErrors = Validate.It(value)
            .Not.Is(Null, Empty, Whitespace)
            .Run();

        var actualError = Assert.Single(actualErrors);
        Assert.Equal(expectedError, actualError);
    }

    [Theory]
    [InlineData("Hi", "value:length(5)")]
    [InlineData("Hello there", "value:length(5)")]
    public void Fail_Length(string? value, string? expectedError)
    {
        var actualErrors = Validate.It(value)
            .Is(Length(5))
            .Run();

        var actualError = Assert.Single(actualErrors);
        Assert.Equal(expectedError, actualError);
    }

    [Theory]
    [InlineData("Hi", "value:min-length(5)")]
    [InlineData("Hello there", "value:max-length(5)")]
    public void Fail_MinLength_MaxLength(string? value, string? expectedError)
    {
        var actualErrors = Validate.It(value)
            .Is(MinLength(5), MaxLength(5))
            .Run();

        var actualError = Assert.Single(actualErrors);
        Assert.Equal(expectedError, actualError);
    }
}
