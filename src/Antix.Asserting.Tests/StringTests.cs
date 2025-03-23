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
                    .Not.Is(Whitespace)
                    .Is(NotWhitespace)
                    .Is(Length(5), MinLength(2), MaxLength(10))
                .Failure(out var actualErrors)
            );

        Assert.Empty(actualErrors);
    }

    [Theory]
    [InlineData(null, "value:not-null")]
    [InlineData("", "value:not-length(0)")]
    [InlineData(" ", "value:not-whitespace")]
    public void Fail_Null_Empty_Whitespace(string? value, string? expectedError)
    {
        var context = Validate.It(value)
            .Not.Is(Null, Whitespace, Length(0));

        var actualErrors = context.Errors;

        var actualError = Assert.Single(actualErrors);
        Assert.Equal(expectedError, actualError);
    }

    [Theory]
    [InlineData("Hi", "value:length(5)")]
    [InlineData("Hello there", "value:length(5)")]
    public void Fail_Length(string? value, string expectedError)
    {
        var actualErrors = Validate.It(value)
            .Is(Length(5))
            .Errors;

        var actualError = Assert.Single(actualErrors);
        Assert.Equal(expectedError, actualError);
    }

    [Theory]
    [InlineData(null, "value:not-null")]
    [InlineData("     ", "value:not-whitespace")]
    [InlineData("Hi", "value:min-length(5)")]
    [InlineData("Hello there", "value:max-length(5)")]
    public void Fail_MinLength_MaxLength(string? value, string expectedError)
    {
        var actualErrors = Validate.It(value)
            .Is(NotNull, NotWhitespace, MinLength(5), MaxLength(5))
            .Errors;

        var actualError = Assert.Single(actualErrors);
        Assert.Equal(expectedError, actualError);
    }
}
