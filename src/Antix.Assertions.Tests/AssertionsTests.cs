namespace Antix.Assertions.Tests;

public sealed class AssertionsTests
{

    [Theory]
    [InlineData("VALUE", false, null)]
    [InlineData("VALUE", true, "not-null")]
    [InlineData(null, false, "null")]
    [InlineData(null, true, null)]
    public void Assertion(string? value, bool negate, string? expected)
    {
        var assertion = new Assertion<string>()
        {
            Test = () => value is not null,
            Negate = negate,
            FailMessage = "null"
        };

        Assert.Equal(expected, assertion.Run());
    }

    [Fact]
    public void Specific_FailNotMessage()
    {

        var assertion = new Assertion<object>()
        {
            Test = () => true,
            Negate = true,
            FailMessage = "Rhubarb",
            FailNotMessage = "Custard"
        };

        Assert.Equal(assertion.FailNotMessage, assertion.Run());
    }
}
