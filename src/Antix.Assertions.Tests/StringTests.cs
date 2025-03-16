using System.Linq;

namespace Antix.Assertions.Tests;

public sealed class StringTests
{
    [Theory]
    [InlineData(null, "value:not-null")]
    [InlineData("", "value:not-empty")]
    [InlineData(" ", "value:not-whitespace")]
    public void String_Null_Empty_Whitespace(string? value, string? expectedError)
    {
        var actualError = It
            .For(value).Assert(It.Is.Not.Null, It.Is.Not.Empty, It.Is.Not.Whitespace)
                .Validate()
                .SingleOrDefault();

        Assert.Equal(expectedError, actualError);
    }

    [Theory]
    [InlineData("Hi", "value:length(5)")]
    [InlineData("Hello", null)]
    [InlineData("Hello there", "value:length(5)")]
    public void String_Length(string? value, string? expectedError)
    {
        var actualError = It
            .For(value).Assert(It.Is.Length(5))
            .Validate()
            .SingleOrDefault();

        Assert.Equal(expectedError, actualError);
    }

    [Theory]
    [InlineData("Hi", "value:min-length(5)")]
    [InlineData("Hello", null)]
    [InlineData("Hello there", "value:max-length(5)")]
    public void String_MinLength_MaxLength(string? value, string? expectedError)
    {
        var actualError = It
            .For(value).Assert(It.Is.MinLength(5), It.Is.MaxLength(5))
            .Validate()
            .SingleOrDefault();

        Assert.Equal(expectedError, actualError);
    }
}
