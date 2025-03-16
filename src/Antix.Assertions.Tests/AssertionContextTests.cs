namespace Antix.Assertions.Tests;

public sealed class AssertionContextTests
{
    [Fact]
    public void Multi_Value()
    {
        var value1 = "VALUE_1";
        var value2 = "VALUE_2";

        var actualErrors = It
            .For(value1).Assert(It.Is.MaxLength(1))
            .For(value2).Assert(It.Is.MaxLength(2))
                .Validate();

        Assert.Equal(2, actualErrors.Length);
        Assert.Equal("value1:max-length(1)", actualErrors[0]);
        Assert.Equal("value2:max-length(2)", actualErrors[1]);
    }
}
