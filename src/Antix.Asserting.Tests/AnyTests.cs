using static Antix.Asserting.Is;
namespace Antix.Asserting.Tests;

public sealed class AnyTests
{
    [Fact]
    public void Happy()
    {
        const string value = "VALUE";

        var actualErrors = Validate
            .It(value).Not.Is(Null)
            .It(value).Is(NotNull)
            .Errors;

        Assert.Empty(actualErrors);
    }

    [Fact]
    public void Fail_Null()
    {
        const string value = "VALUE";

        Assert.True(
            Validate
                .It(value)
                    .Is(Null)
                .Failure(out var actualErrors)
            );

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("value:null", actualError);
    }

    [Fact]
    public void Fail_NotNull()
    {
        var value = default(string);

        Assert.True(
            Validate
                .It(value)
                    .Is(NotNull)
                .Failure(out var actualErrors)
            );

        var actualError = Assert.Single(actualErrors);
        Assert.Equal("value:not-null", actualError);
    }
}
