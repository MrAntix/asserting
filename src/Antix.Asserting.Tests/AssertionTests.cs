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
        var assertion = new Assertion()
        {
            TestSuccess = () => value is not null,
            FailMessage = "null"
        };

        Assert.Equal(expected, assertion.Run(negate));
    }

    [Fact]
    public void Specific_FailNotMessage()
    {

        var assertion = new Assertion()
        {
            TestSuccess = () => true,
            FailMessage = "Rhubarb",
            FailNotMessage = "Custard"
        };

        Assert.Equal(assertion.FailNotMessage, assertion.Run(true));
    }
}
