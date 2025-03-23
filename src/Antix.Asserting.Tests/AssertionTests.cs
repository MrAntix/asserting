namespace Antix.Asserting.Tests;

public sealed class AssertionTests
{

    [Theory]
    [InlineData("VALUE", false, null)]
    [InlineData("VALUE", true, "not-null")]
    [InlineData(null, false, "null")]
    [InlineData(null, true, null)]
    public void Assertion(string? value, bool negate, string? expected)
    {
        var assertion = new MaybeNullAssertion()
        {
            Success = value is not null,
            FailMessage = "null"
        };

        Assert.Equal(expected, assertion.Run(negate));
    }

    [Fact]
    public void Specific_FailNotMessage()
    {

        var assertion = new MaybeNullAssertion()
        {
            Success = true,
            FailMessage = "Rhubarb",
            FailNotMessage = "Custard"
        };

        Assert.Equal(assertion.FailNotMessage, assertion.Run(true));
    }
}
